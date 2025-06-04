using UnityEngine;

public class EnemyMovementManual : MonoBehaviour
{
    [Header("Cài đặt AI")]
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 10f;
    public float attackRange = 8f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

    [Header("Animation")]
    public Animator animator;

    private float lastAttackTime;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Tự động gán nếu quên kéo trong Inspector
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            if (distance > attackRange)
            {
                MoveTowardsPlayer();

                if (animator != null)
                {
                    animator.SetFloat("Speed", moveSpeed);
                    animator.SetBool("IsAttack", false);
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetFloat("Speed", 0f);
                    animator.SetBool("IsAttack", true);
                }

                TryAttack();
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
                animator.SetBool("IsAttack", false);
            }
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

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Gọi animation trigger Shoot (nếu có Animation Event sẽ gọi DealDamage)
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }
        }
    }

    // Hàm này nên được gọi bằng animation event tại thời điểm ra đòn
    public void DealDamage()
    {
        if (player != null)
        {
            HEALTH health = player.GetComponent<HEALTH>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
