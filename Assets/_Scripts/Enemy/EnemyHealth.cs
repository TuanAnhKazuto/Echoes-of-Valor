using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Cài đặt máu")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Hiệu ứng chết")]
    public Animator animator;
    public GameObject deathEffect; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die"); 
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 2f); 
    }
}
