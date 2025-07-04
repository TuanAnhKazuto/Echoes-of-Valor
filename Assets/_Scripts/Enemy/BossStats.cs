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
    public TextMeshProUGUI nameText;
    public GameObject bossUIRoot;

    [Header("Base Stats")]
    public int level = 1;
    public int maxHealth = 10;
    public int currentHealth;
    public float baseDamage = 10f;

    [Header("LevelUp")]
    public float healthPerLevel = 20f;
    public float damagePerLevel = 5f;

    [Header("Health Bar Settings")]
    public float showHealthRange = 10f;

    [HideInInspector] public bool isLowHealth = false;
    private bool hasTransformed = false;
    private bool isPlayerNear = false;

    [SerializeField] private string questTag = "Boss";

    private SkeletonNecromancer enemyAI;
    private Transform player;

    private void Awake()
    {
        if (characterStats == null)
            characterStats = FindAnyObjectByType<CharacterStats>();

        enemyAI = GetComponent<SkeletonNecromancer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);

        HideHealthUI();
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= showHealthRange && !isPlayerNear)
            {
                ShowHealthUI();
            }
            else if (distance > showHealthRange && isPlayerNear)
            {
                HideHealthUI();
            }
        }
    }

    private void ShowHealthUI()
    {
        isPlayerNear = true;

        if (bossUIRoot != null && !bossUIRoot.activeSelf)
            bossUIRoot.SetActive(true);
    }

    private void HideHealthUI()
    {
        isPlayerNear = false;

        if (bossUIRoot != null && bossUIRoot.activeSelf)
            bossUIRoot.SetActive(false);
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
        if (currentHealth <= 0) return;

        currentHealth -= (int)damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            HandleDeath();
            return;
        }

        if (!isLowHealth && currentHealth <= maxHealth / 2)
        {
            isLowHealth = true;
            enemyAnimator?.SetBool("LowHealth", true);

            if (!hasTransformed)
            {
                baseDamage += 10;
                if (enemyAI != null)
                {
                    enemyAI.BoostSpeed(5f);
                }
                hasTransformed = true;
            }
        }
    }

    private void HandleDeath()
    {
        HideHealthUI();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            var playerQuest = playerObj.GetComponent<PlayerQuest>();
            if (playerQuest != null)
            {
                playerQuest.UpdateQuest(questTag);
            }
        }

        if (enemyAI != null)
        {
            enemyAI.Die(); // Gọi animator trigger "Die" và destroy sau đó
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            TakeDamage(characterStats.TotalDamage);
        }

        if (other.CompareTag("PlayerSkill"))
        {
            SkillInfo skillDamage = other.GetComponent<SkillInfo>();
            if (skillDamage != null)
                TakeDamage(skillDamage.damgeSkill);
        }
    }
}
