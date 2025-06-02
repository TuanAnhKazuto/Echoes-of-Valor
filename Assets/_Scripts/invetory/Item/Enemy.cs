using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50;

    public void TakeDamage(float  amount)
    {
        health -= amount;
        Debug.Log("Enemy HP: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
