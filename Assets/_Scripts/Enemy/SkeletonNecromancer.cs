using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SkeletonNecromancer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float radius = 10f;
    public float maxDistance = 50f;
    public Animator animator;

    public float attackRange = 2f;
    public float rotationSpeed = 5f;
    public float attackCooldown = 2f;
    private float lastAttackTime;

    private Transform target;
    private Vector3 originalePosition;

    private BossStats bossStats;
    private Coroutine spinAttackRoutine;
    private bool isDead = false;
    private bool isTrackingPlayer = false;
    public enum CharacterState
    {
        Normal,
        Attack,
        SpinAttack
    }
    public CharacterState currentState;

    [Header("DropItem")]
    public GameObject dropItem;


    [Header("Audio Settings")]
    public AudioClip attackSound;
    public AudioClip spinAttackSound;
    public AudioClip hitSound;
    public AudioClip footstepSound; // ✅ Âm thanh bước chân
    public AudioClip deathSound;    // ✅ Âm thanh chết
    private AudioSource audioSource;
    private bool isPlayingFootstep = false; // ✅ Kiểm soát âm thanh bước chân

    void Start()
    {
        originalePosition = transform.position;
        bossStats = GetComponent<BossStats>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(FindPlayerTarget());
    }

    private IEnumerator FindPlayerTarget()
    {
        while (target == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
                isTrackingPlayer = true;
            }

            yield return null;
        }
    }

    void Update()
    {
        if (isDead || !isTrackingPlayer || target == null) return;

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        float distanceToOrigin = Vector3.Distance(originalePosition, transform.position);

        bool lowHealth = bossStats != null && bossStats.isLowHealth;

        if (!lowHealth && (distanceToTarget > radius || distanceToOrigin > maxDistance))
        {
            ReturnToOrigin();
            return;
        }

        if (distanceToTarget > attackRange)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);

            float moveSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", moveSpeed);
            StopSpinIfNeeded();
            ChangeState(CharacterState.Normal);
        }
        else
        {
            navMeshAgent.isStopped = true;
            animator.SetFloat("Speed", 0);
            RotateTowardsTarget();

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (lowHealth)
                {
                    if (spinAttackRoutine == null)
                    {
                        spinAttackRoutine = StartCoroutine(PlaySpinAttack());
                    }
                }
                else
                {
                    ChangeState(CharacterState.Attack);
                }

                lastAttackTime = Time.time;
            }
        }

        // ✅ Tắt âm thanh bước chân khi dừng di chuyển
        if (navMeshAgent.velocity.magnitude < 0.1f && isPlayingFootstep)
        {
            StopFootstepSound();
        }
    }

    void ReturnToOrigin()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(originalePosition);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        if (Vector3.Distance(transform.position, originalePosition) < 1f)
        {
            animator.SetFloat("Speed", 0);
        }

        animator.SetBool("Attack", false);
        animator.SetBool("IsSpinning", false);
        ChangeState(CharacterState.Normal);
        StopSpinIfNeeded();
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f;
        if (direction.magnitude > 0f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void ChangeState(CharacterState newState)
    {
        if (currentState == newState && newState != CharacterState.Attack)
            return;

        animator.SetBool("Attack", newState == CharacterState.Attack);
        animator.SetBool("IsSpinning", newState == CharacterState.SpinAttack);

        currentState = newState;
    }

    private IEnumerator PlaySpinAttack()
    {
        ChangeState(CharacterState.SpinAttack);
        animator.SetBool("IsSpinning", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("IsSpinning", false);
        ChangeState(CharacterState.Normal);

        spinAttackRoutine = null;
    }

    private void StopSpinIfNeeded()
    {
        if (spinAttackRoutine != null)
        {
            StopCoroutine(spinAttackRoutine);
            spinAttackRoutine = null;
            animator.SetBool("IsSpinning", false);
        }
    }

    public void BoostSpeed(float amount)
    {
        navMeshAgent.speed += amount;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Die");

        StopSpinIfNeeded();

        PlayDeathSound(); // ✅ Phát âm chết (hoặc dùng animation event)

        this.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        DropItem();

        Destroy(gameObject, 3f);
    }

    public void DropItem()
    {
        Vector3 dropPos = new();

        dropPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        GameObject item = Instantiate(dropItem, dropPos, Quaternion.identity);
    }

    public void DealDamageToPlayer()
    {
        if (target != null && bossStats != null)
        {
            CharacterStats playerStats = target.GetComponent<CharacterStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(bossStats.baseDamage);

                if (hitSound != null && audioSource != null)
                {
                    audioSource.pitch = Random.Range(0.95f, 1.05f);
                    audioSource.PlayOneShot(hitSound);
                    audioSource.pitch = 1f;
                }
            }
        }
    }

    public void PlaySpinAttackSound()
    {
        if (spinAttackSound != null && audioSource != null)
            audioSource.PlayOneShot(spinAttackSound);
    }

    // ✅ Gọi từ Animation Event tại frame bước chân
    public void PlayFootstepSound()
    {
        if (footstepSound != null && audioSource != null && !isPlayingFootstep)
        {
            audioSource.PlayOneShot(footstepSound);
            isPlayingFootstep = true;
        }
    }

    // ✅ Gọi từ cuối animation step hoặc tự động tắt nếu dừng
    public void StopFootstepSound()
    {
        isPlayingFootstep = false;
    }

    public void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}
