using System.Collections;
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

    public bool isDie = false;

    [Header("LevelUp")]
    public float healthPerLevel = 20f;
    public float damagePerLevel = 5f;

    [Header("Quest")]
    public string questTag = "Enemy_Main";

    [Header("VFX")]
    public GameObject damagePopupPrefab;

    [Header("DropItem")]
    public GameObject dropItem;
    public IngredientPickup ingredientPickup;

    private void Start()
    {
        skeletonMovement = GetComponent<SkeletonMovement>();
        ingredientPickup = GetComponent<IngredientPickup>();

        currentHealth = maxHealth;
        skeletonMovement.animator.SetFloat("HP", currentHealth);
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);

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
        skeletonMovement.animator.SetFloat("HP", currentHealth);

        skeletonMovement.animator.SetLayerWeight(1, 0.7f);
        skeletonMovement.animator.SetBool("Hit", true);

        DamagePopupSpawner.Instance.ShowDamage(transform.position + Vector3.up * 2f, (int)damage, Color.red);// effect - HP

        if (currentHealth <= 0)
        {
            isDie = true;
            currentHealth = 0;
            healthBar.UpdateHealth(currentHealth, maxHealth);
            healthBar.gameObject.SetActive(false);
            healthBar.cursorTarget.SetActive(false);
            skeletonMovement.animator.SetLayerWeight(1, 0f);

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
        }
    }

    public void Death()
    {
        DropItem();
        ingredientPickup.Pickup();
        //StartCoroutine(ingredientPickup.Pickup());
        Destroy(gameObject);  
    }

    public void DropItem()
    {
        Vector3 dropPos = new();

        dropPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        GameObject item = Instantiate(dropItem, dropPos, Quaternion.identity);
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

            TakeDamage(characterStats.TotalDamage);
        }

        if (other.gameObject.CompareTag("PlayerSkill"))
        {
            var skillDamage = other.gameObject.GetComponent<SkillInfo>();
            TakeDamage(skillDamage.damgeSkill);
        }
    }

}
