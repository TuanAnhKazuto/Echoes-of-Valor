using UnityEngine;
using System.Collections;

public class PlayerTarget : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float shootCooldown = 1f;
    public float shootDelay = 0.25f;
    public Transform characterTransform; 

    private float lastShootTime = -Mathf.Infinity;
    private Transform targetEnemy;
    private bool isWaitingToShoot = false;
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetEnemy = FindNearestEnemy(25f);

            if (targetEnemy != null)
            {
                LookAtTarget(targetEnemy);
            }

            if (!isWaitingToShoot && Time.time - lastShootTime >= shootCooldown)
            {
                StartCoroutine(DelayedShoot());
            }
        }
    }

    IEnumerator DelayedShoot()
    {
        isWaitingToShoot = true;
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        yield return new WaitForSeconds(shootDelay);

        if (Time.time - lastShootTime >= shootCooldown)
        {
            ShootArrow();
            lastShootTime = Time.time;
        }

        isWaitingToShoot = false;
    }

    Transform FindNearestEnemy(float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range && dist < minDistance)
            {
                minDistance = dist;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }

    void LookAtTarget(Transform target)
    {
        if (characterTransform == null || target == null) return;

        Vector3 direction = (target.position - characterTransform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            characterTransform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            if (targetEnemy != null)
            {
                arrowScript.SetTarget(targetEnemy);
            }
            else
            {
                arrowScript.SetDirection(transform.forward);
            }
        }
    }
}
