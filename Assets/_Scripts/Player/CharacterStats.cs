using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public PlayerHealthBar healthBar;

    [Header("Base Stats")]
    public int level = 1;
    public float maxHealth = 100f;
    public float currentHealth;
    public float currentMana;
    public float maxMana = 100f;

    public int baseDamage = 10;
    public int baseDefense = 5;

    [Header("Equipment")]
    public WeaponData equippedWeapon;

    private void Awake()
    {
        if (healthBar == null)
        {
            healthBar = FindAnyObjectByType<PlayerHealthBar>();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public int TotalDamage => baseDamage + (equippedWeapon != null ? equippedWeapon.GetDamage() : 0);
    public int TotalDefense => baseDefense + (equippedWeapon != null ? equippedWeapon.GetDefense() : 0);

    public void TakeDamage(float damage)
    {
        float damageTake = Mathf.Max(damage - TotalDefense, 1f);
        currentHealth -= damageTake;
        healthBar.UpdateHealth((int)currentHealth, (int)maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.UpdateHealth((int)currentHealth, (int)maxHealth);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        // Handle death logic here, e.g., respawn, game over, etc.
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.CompareTag("EnemyHitBox"))
        {
            TakeDamage(20f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            Heal(10f); 
        }
    }
}