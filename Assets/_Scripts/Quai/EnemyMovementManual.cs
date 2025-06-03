using UnityEngine;

public class EnemyMovementManual : MonoBehaviour
{
    [Header("Cài đặt AI")]
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

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

                // Gán tốc độ cho Animator để chuyển sang animation "Run"
                if (animator != null)
                    animator.SetFloat("Speed", moveSpeed);
            }
            else
            {
                if (animator != null)
                    animator.SetFloat("Speed", 0f); // Dừng chạy

                Attack();
            }
        }
        else
        {
            // Ngoài phạm vi, dừng di chuyển
            if (animator != null)
                animator.SetFloat("Speed", 0f);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            if (animator != null)
                animator.SetTrigger("attack"); // Bạn đang dùng trigger 'attack' => ok

            HEALTH health = player.GetComponent<HEALTH>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
