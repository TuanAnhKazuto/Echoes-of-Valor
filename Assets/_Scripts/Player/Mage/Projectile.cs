using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    private float damage;

    public void Initialize(float damageAmount)
    {
        damage = damageAmount;
        Destroy(gameObject, 3f); 
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); 
        }
        else if (!other.CompareTag("Player")) 
        {
            Destroy(gameObject); 
        }
    }
}
