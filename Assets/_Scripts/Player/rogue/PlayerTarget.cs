using UnityEngine;
using System.Collections;

public class PlayerTarget : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float shootCooldown = 2f; // bug 
    public float shootDelay = 0.75f;   

    private float lastShootTime = -Mathf.Infinity;
    private Transform targetEnemy;
    private bool isWaitingToShoot = false;

    void Update()
    {
        FindNearestEnemy();

        if (targetEnemy != null)
        {
            RotateTowardsTarget(targetEnemy.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isWaitingToShoot && Time.time - lastShootTime >= shootCooldown)
            {
                StartCoroutine(DelayedShoot());
            }
        }
    }

    IEnumerator DelayedShoot()
    {
        isWaitingToShoot = true;
        yield return new WaitForSeconds(shootDelay); 
        if (Time.time - lastShootTime >= shootCooldown)
        {
            ShootArrow();
            lastShootTime = Time.time;
        }

        isWaitingToShoot = false;
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= 13f && dist < minDistance)
            {
                minDistance = dist;
                nearest = enemy.transform;
            }
        }

        targetEnemy = nearest;
    }

    void RotateTowardsTarget(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
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
