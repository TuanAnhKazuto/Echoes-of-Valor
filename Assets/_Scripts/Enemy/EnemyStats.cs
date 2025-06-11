using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Component")]
    public PlayerData playerData;
    public CharacterStats characterStats;

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

    private void Awake()
    {
        if (characterStats == null)
        {
            characterStats = FindAnyObjectByType<CharacterStats>();
        }
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
        currentHealth = maxHealth; // Reset current health to max health on level up
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        healthBar.UpdateHealth(currentHealth, maxHealth);

        if(currentHealth < 0)
        {
            Destroy(gameObject);
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
