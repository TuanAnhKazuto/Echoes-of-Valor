using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Cài đặt máu")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Hiệu ứng chết")]
    public Animator animator;
    public GameObject deathEffect; // ví dụ: prefab hiệu ứng nổ, tan biến, v.v.

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Gọi hàm này khi quái bị sát thương
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
            animator.SetTrigger("Die"); // trigger animation chết
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Có thể thêm delay trước khi huỷ
        Destroy(gameObject, 2f); // huỷ quái sau 2 giây
    }
}
