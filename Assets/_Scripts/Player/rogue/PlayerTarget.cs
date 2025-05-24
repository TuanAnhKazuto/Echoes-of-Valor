using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public GameObject arrowPrefab;      // Prefab mũi tên
    public Transform shootPoint;        // Điểm bắn mũi tên (ví dụ đầu cung)
    public float shootCooldown = 1f;    // Thời gian giữa các lần bắn
    public float cooldownAfterShoot = 2f; // Thời gian hồi chiêu sau khi bắn

    private float lastShootTime = -Mathf.Infinity;
    private bool canShoot = true;

    private Transform targetEnemy;

    void Update()
    {
        FindNearestEnemy();

        if (targetEnemy != null)
        {
            RotateTowardsTarget(targetEnemy.position);
        }

        if (Input.GetMouseButtonDown(0)) // Chuột trái
        {
            if (canShoot && Time.time - lastShootTime >= shootCooldown)
            {
                ShootArrow();
            }
        }
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
        direction.y = 0; // Giữ nguyên trục Y để nhân vật chỉ xoay ngang
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
    void ShootArrow()
    {
        // Tạo mũi tên ở điểm bắn
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
                arrowScript.SetDirection(transform.forward); // Bay thẳng
            }
        }

        lastShootTime = Time.time;
        canShoot = false;

        // Bắt đầu hồi chiêu
        Invoke(nameof(ResetShoot), cooldownAfterShoot);
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}
