using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMovement : MonoBehaviour
{
    public float radiusLookAt = 10f;
    public float attackRange = 8f;
    private float originalSpeed = 6f;

    [Header("Patrol Settings")]
    public float patrolRange = 5f;       // độ rộng cạnh hình vuông
    public float patrolWaitTime = 2f;    // dừng lại ở mỗi điểm
    private Vector3[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool isWaiting = false;

    Vector3 startTransform;

    public bool isSpawned = false;
    SaveGameManager saveGameManager;
    EnemyStats enemyStats;

    NavMeshAgent navAgent;
    public Transform player;
    public Animator animator;

    [Range(0, 360)]
    public float agent;
    public float ditectionRadius;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer = false;

    private void Awake()
    {
        saveGameManager = FindAnyObjectByType<SaveGameManager>();

        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();

        startTransform = transform.position;

        // tạo 4 điểm hình vuông quanh chỗ spawn
        patrolPoints = new Vector3[4];
        patrolPoints[0] = startTransform + new Vector3(patrolRange, 0, patrolRange);
        patrolPoints[1] = startTransform + new Vector3(-patrolRange, 0, patrolRange);
        patrolPoints[2] = startTransform + new Vector3(-patrolRange, 0, -patrolRange);
        patrolPoints[3] = startTransform + new Vector3(patrolRange, 0, -patrolRange);
    }

    private void Start()
    {
        StartCoroutine(FOVRoutime());
    }

    private void Update()
    {
        if (saveGameManager.isCharacterSpawned)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn_Ground_Skeletons"))
        {
            isSpawned = true;
        }
        if (!isSpawned) return;
        if (enemyStats.isDie) return;

        Movement();
    }

    IEnumerator FOVRoutime()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return waitTime;
            FielOfViewCheck();
        }
    }

    private void FielOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusLookAt, targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= ditectionRadius)
            {
                canSeePlayer = true;
                animator.SetBool("Ditection", true);
                return;
            }

            if (Vector3.Angle(transform.forward, directionToTarget) < agent / 2)
            {
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    animator.SetBool("Ditection", true);
                }
                else
                {
                    canSeePlayer = false;
                    animator.SetBool("Ditection", false);
                }
            }
            else
            {
                canSeePlayer = false;
                animator.SetBool("Ditection", false);
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            animator.SetBool("Ditection", false);
        }
    }

    public void Movement()
    {
        float distance = player != null ? Vector3.Distance(transform.position, player.position) : Mathf.Infinity;

        if (!canSeePlayer)
        {
            PatrolSquare();
            return;
        }

        if (distance <= radiusLookAt)
        {
            navAgent.SetDestination(player.position);
            animator.SetFloat("Speed", navAgent.velocity.magnitude);

            if (distance <= attackRange)
            {
                animator.SetBool("IsAttack", true);
                navAgent.speed = 0f;

                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                animator.SetBool("IsAttack", false);
                navAgent.speed = originalSpeed;
            }
        }
    }

    private void PatrolSquare()
    {
        if (patrolPoints.Length == 0) return;
        if (isWaiting) return;

        animator.SetBool("IsAttack", false);

        Vector3 targetPoint = patrolPoints[currentPatrolIndex];
        navAgent.speed = originalSpeed;
        navAgent.SetDestination(targetPoint);
        animator.SetFloat("Speed", navAgent.velocity.magnitude);

        // Nếu tới gần điểm patrol thì chuyển sang điểm tiếp theo
        if (Vector3.Distance(transform.position, targetPoint) < 1f)
        {
            StartCoroutine(WaitAndGoNext());
        }

        // Nếu bị tường cản (không di chuyển được)
        if (navAgent.remainingDistance > 1.5f && navAgent.velocity.magnitude < 0.05f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navAgent.SetDestination(patrolPoints[currentPatrolIndex]);
        }
    }

    IEnumerator WaitAndGoNext()
    {
        isWaiting = true;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(patrolWaitTime);

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        isWaiting = false;
    }
}
