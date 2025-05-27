using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int amount)
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
