using TMPro;
using UnityEngine;
using System.Collections;

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
    public int expReward = 100;
    public int maxHealth = 10;
    public int currentHealth;
    public float baseDamage = 10f;

    [Header("LevelUp")]
    public float healthPerLevel = 20f;
    public float damagePerLevel = 5f;

    [Header("Health Bar Settings")]
    public float showHealthRange = 10f;

    [Header("Low Health VFX")]
    public GameObject lowHealthVFXPrefab;
    private GameObject activeLowHealthVFX;

    [HideInInspector] public bool isLowHealth = false;
    private bool hasTransformed = false;
    private bool isPlayerNear = false;

    [SerializeField] private string questTag = "Boss";

    private SkeletonNecromancer enemyAI;
    private Transform player;

    private void Awake()
    {
        enemyAI = GetComponent<SkeletonNecromancer>();
        StartCoroutine(FindPlayerAndStats());

        // Ngăn VFX chạy ngay khi load
        if (activeLowHealthVFX != null)
            Destroy(activeLowHealthVFX);
    }

    private IEnumerator FindPlayerAndStats()
    {
        while (player == null || characterStats == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                characterStats = playerObj.GetComponent<CharacterStats>();
            }
            yield return null;
        }
    }

    private void Start()
    {
        lowHealthVFXPrefab.SetActive(false);
        currentHealth = maxHealth;

        if (bossUIRoot == null)
            bossUIRoot = transform.Find("BossUIRoot")?.gameObject;

        if (healthBar == null)
            healthBar = GetComponentInChildren<EnemyHealthBar>();

        if (levelText == null && bossUIRoot != null)
            levelText = bossUIRoot.GetComponentInChildren<TextMeshProUGUI>();

        if (levelText != null)
            levelText.text = "Lv. " + level.ToString();

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);

        HideHealthUI();

        // Đảm bảo tắt VFX ngay khi bắt đầu
        if (activeLowHealthVFX != null)
            Destroy(activeLowHealthVFX);
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

        if (levelText != null)
            levelText.text = "Lv. " + level.ToString();

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= (int)damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            HandleDeath();
            characterStats.expManager.AddExp(expReward);
            return;
        }

        // Nếu máu < 50% và chưa ở trạng thái LowHealth
        if (!isLowHealth && currentHealth <= maxHealth / 2)
        {
            isLowHealth = true;
            enemyAnimator?.SetBool("LowHealth", true);

            // ✅ Bật VFX
            if (lowHealthVFXPrefab != null && activeLowHealthVFX == null)
            {
                lowHealthVFXPrefab.SetActive(true);
                
            }

            // ✅ Tăng sức mạnh
            if (!hasTransformed)
            {
                baseDamage += 10;
                if (enemyAI != null)
                    enemyAI.BoostSpeed(5f);
                hasTransformed = true;
            }
        }
    }

    private void HandleDeath()
    {
        HideHealthUI();

        if (activeLowHealthVFX != null)
            Destroy(activeLowHealthVFX); // ✅ Tắt VFX khi chết

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            var playerQuest = playerObj.GetComponent<PlayerQuest>();
            if (playerQuest != null)
                playerQuest.UpdateQuest(questTag);
        }

        if (enemyAI != null)
            enemyAI.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            if (characterStats != null)
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
