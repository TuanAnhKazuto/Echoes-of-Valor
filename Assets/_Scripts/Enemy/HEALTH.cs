using UnityEngine;

public class HEALTH : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public HealthBar healthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player took damage: " + amount);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Xử lý chết tại đây
    }
}
