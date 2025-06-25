using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Component")]
    public CharacterStats characterStats;
    public SkeletonMovement skeletonMovement;

    public EnemyHealthBar healthBar;
    public TextMeshProUGUI levelText;

    [Header("Base Stats")]
    public int level = 1;
    public int maxHealth = 10;
    public int currentHealth;

    public float baseDamage = 10f;

    [Header("LevelUp")]
    public float healthPerLevel = 20f;
    public float damagePerLevel = 5f;

    [Header("Quest")]
    public string questTag = "Enemy_Main";

    private void Start()
    {
        skeletonMovement = GetComponent<SkeletonMovement>();

        currentHealth = maxHealth;
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    public void LevelUp()
    {
        level++;
        maxHealth += (int)healthPerLevel;
        baseDamage += damagePerLevel;
        currentHealth = maxHealth; // Reset current health to max health on level up
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        healthBar.UpdateHealth(currentHealth, maxHealth);

        skeletonMovement.animator.SetLayerWeight(1, 0.7f);
        skeletonMovement.animator.SetBool("Hit", true); 

        if (currentHealth <= 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerQuest = player.GetComponent<PlayerQuest>();
                if (playerQuest != null)
                {
                    playerQuest.UpdateQuest(questTag);
                    Debug.Log($"Cập nhật nhiệm vụ với questTag: {questTag}");
                }
            }

            Destroy(gameObject);
        }
    }

    public void OffTakeDamageAnim()
    {
        skeletonMovement.animator.SetLayerWeight(1, 0f); 
        skeletonMovement.animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            if (characterStats == null)
            {
                characterStats = other.GetComponentInParent<CharacterStats>();
            }

            TakeDamage(characterStats.baseDamage);
        }

        if (other.gameObject.CompareTag("PlayerSkill"))
        {
            var skillDamage = other.gameObject.GetComponent<SkillInfo>();
            TakeDamage(skillDamage.damgeSkill);
        }
    }

}
