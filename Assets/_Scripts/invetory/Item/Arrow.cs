using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target;
    private Vector3 direction;
    private bool hasTarget = false;
    private bool isFlying = false;

    public float speed = 15f;
    public Vector3 arrowRotationOffset = new Vector3(0, 90, 0);

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
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(arrowRotationOffset);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowRotationOffset);
        }
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
