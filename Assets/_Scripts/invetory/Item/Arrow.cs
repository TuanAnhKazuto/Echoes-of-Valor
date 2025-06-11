using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target;
    private Vector3 direction;
    private bool hasTarget = false;
    private bool isFlying = false;

    public float speed = 15f;
    public float damage = 20f;
    public Vector3 arrowRotationOffset = Vector3.zero;

    public ParticleSystem hitEffect;
    public ParticleSystem aoeImpactEffect;

    public void SetTarget(Transform enemy)
    {
        target = enemy;
        hasTarget = true;
        isFlying = true;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        hasTarget = false;
        isFlying = true;
    }

    void Update()
    {
        if (!isFlying) return;

        if (hasTarget)
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 targetPos = target.position + Vector3.up * 1.5f;
            Vector3 dir = targetPos - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget(target.gameObject);
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(arrowRotationOffset + new Vector3(0, 180f, 0));
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowRotationOffset + new Vector3(0, 180f, 0));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isFlying) return;

        if (other.CompareTag("Enemy"))
        {
            HitTarget(other.gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (aoeImpactEffect != null)
                Instantiate(aoeImpactEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    void HitTarget(GameObject enemy)
    {
        EnemyStats health = enemy.GetComponent<EnemyStats>();
        if (health != null)
        {
            health.TakeDamage((int)damage);
        }

        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        if (aoeImpactEffect != null)
            Instantiate(aoeImpactEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
