using UnityEngine;

public class EnemyMovementManual : MonoBehaviour
{
    [Header("Cài đặt AI")]
    public Transform player { get; set; }
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

        player = FindObjectOfType<PlayerController>().transform;
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
            Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z);
            Quaternion toRotation = Quaternion.LookRotation(flatDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);

            // Giữ chân chạm đất (chỉ xoay quanh trục Y)
            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
        }
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            DealDamage();
        }

        DirectPlayer();
    }

    private void DirectPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z);
            Quaternion toRotation = Quaternion.LookRotation(flatDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);

            // Giữ chân chạm đất (chỉ xoay quanh trục Y)
            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
        }
    }

    public virtual void DealDamage()
    {
        Debug.Log("damage");

        if (player != null)
        {
            CharacterStats health = player.GetComponent<CharacterStats>();
            if (health != null)
            {
                health.TakeDamage(damage);
                SpawnBullet(health);
            }
        }
    }

    public virtual void SpawnBullet(CharacterStats characterStats)
    {
    }

    public void Attack()
    {
    }

    public void OnAniStart()
    {
    }

    public void OnAniEnd()
    {
    }
}
