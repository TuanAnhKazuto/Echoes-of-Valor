using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [Header("Component")]
    public PlayerData playerData;
    public CharacterStats characterStats;

    public HealthBar healthBar;
    public TextMeshProUGUI levelText;

    [Header("Base Stats")]
    public int level = 1;
    public int maxHealth = 100;
    public int currentHealth;

    public float baseDamage = 10f;

    [Header("LevelUp")]
    public float healthPerLevel = 20f;
    public float damagePerLevel = 5f;

    private void Awake()
    {
        if(characterStats == null)
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage();
            Debug.Log($"Hp = {maxHealth}.\nDamage taked = {characterStats.TotalDamage}");
        }
    }

    public void TakeDamage()
    {
        currentHealth -= characterStats.TotalDamage;
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            TakeDamage();
        }
    }
}
