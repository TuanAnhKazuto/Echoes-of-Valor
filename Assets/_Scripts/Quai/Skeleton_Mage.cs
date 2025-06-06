using UnityEngine;

public class Skeleton_Magel : MonoBehaviour
{
    [Header("Cài đặt AI")]
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 10f;
    public float attackRange = 8f;
    public float attackCooldown = 1.5f;
    public GameObject arrowPrefab;
    public Transform firePoint;

    [Header("Animation (nếu có)")]
    public Animator animator;

    private float lastAttackTime;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            if (distance > attackRange)
            {
                MoveTowardsPlayer();
                animator?.SetFloat("Speed", moveSpeed);
                animator?.SetBool("IsAttack", false);
            }
            else
            {
                animator?.SetFloat("Speed", 0f);
                animator?.SetBool("IsAttack", true);

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                    animator?.SetTrigger("Shoot");
                }
            }
        }
        else
        {
            animator?.SetFloat("Speed", 0f);
            animator?.SetBool("IsAttack", false);
        }

        RotateTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
        }
    }

    // Gọi từ Animation Event trong animation
    public void OnAttack()
    {
        Attack();
    }

    // Gọi từ Animation Event khi kết thúc animation tấn công
    public void EndAttack()
    {
        Debug.Log("EndAttack Animation Event Triggered");
    }

    // Hàm tấn công thực tế - bắn tên
    public void Attack()
    {
        if (arrowPrefab != null && firePoint != null)
        {
            Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
