using UnityEngine;

public class CharacterStats : MonoBehaviour
{
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

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        // Handle death logic here, e.g., respawn, game over, etc.
    }
}