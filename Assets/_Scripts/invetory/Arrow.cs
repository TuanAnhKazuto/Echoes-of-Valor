using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform target;
    private Vector3 direction;
    private bool hasTarget = false;

    public float speed = 15f;

    public void SetTarget(Transform enemy)
    {
        target = enemy;
        hasTarget = true;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        hasTarget = false;
    }

    void Update()
    {
        if (hasTarget)
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            // Bay thẳng
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
