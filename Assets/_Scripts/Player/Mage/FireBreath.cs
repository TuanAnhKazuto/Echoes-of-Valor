using UnityEngine;

public class FireBreath : MonoBehaviour
{
    public float damagePerSecond = 1f;
    public float tickRate = 0.5f;
    public LayerMask enemyLayer;

    private void Start()
    {
        InvokeRepeating(nameof(DealDamage), 0f, tickRate);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null) 
                enemyStats.TakeDamage(damagePerSecond);
        }
    }

    void DealDamage()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation, enemyLayer);
        foreach (var hit in hits)
        {
            EnemyStats enemy = hit.GetComponent<EnemyStats>();
            if (enemy != null)
                enemy.TakeDamage(damagePerSecond * tickRate);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
