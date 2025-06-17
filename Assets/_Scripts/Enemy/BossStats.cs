using TMPro;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    [Header("Component")]
    public PlayerData playerData;
    public CharacterStats characterStats;

    [Header("AI Control")]
    public Animator enemyAnimator;
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

    [HideInInspector] public bool isLowHealth = false;
    private bool hasTransformed = false;

    private SkeletonNecromancer enemyAI;

    private void Awake()
    {
        if (characterStats == null)
        {
            characterStats = FindAnyObjectByType<CharacterStats>();
        }

        enemyAI = GetComponent<SkeletonNecromancer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void LevelUp()
    {
        level++;
        maxHealth += (int)healthPerLevel;
        baseDamage += damagePerLevel;
        currentHealth = maxHealth;
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerQuest = player.GetComponent<PlayerQuest>();
                if (playerQuest != null)
                {
                    playerQuest.UpdateQuest(gameObject.tag);
                }
            }

            Destroy(gameObject);
        }

        // Kích hoạt trạng thái LowHealth
        if (!isLowHealth && currentHealth <= maxHealth / 2)
        {
            isLowHealth = true;
            enemyAnimator?.SetBool("LowHealth", true);

            if (!hasTransformed)
            {
                baseDamage += 10; // Tăng 10 sát thương
                if (enemyAI != null)
                {
                    enemyAI.BoostSpeed(5f); // Tăng tốc độ chạy
                }
                hasTransformed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            TakeDamage(characterStats.TotalDamage);
        }

        if (other.gameObject.CompareTag("PlayerSkill"))
        {
            var skillDamage = other.gameObject.GetComponent<SkillInfo>();
            TakeDamage(skillDamage.damgeSkill);
        }
    }
}
