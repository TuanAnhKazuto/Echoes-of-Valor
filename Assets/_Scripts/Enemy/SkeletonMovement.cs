using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMovement : MonoBehaviour
{
    public float radiusLookAt = 10f;
    public float attackRange = 8f;
    private float originalSpeed;

    [Header("Square Patrol Settings")]
    public float patrolRange = 5f;      // độ rộng cạnh hình vuông
    public float patrolWaitTime = 2f;   // dừng lại mỗi góc
    private int currentPatrolIndex = 0;
    private bool isWaiting = false;
    private Vector3[] patrolPoints;

    Vector3 startPosition;
    Quaternion startRotation;

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

        startPosition = transform.position;
        startRotation = transform.rotation;

        originalSpeed = navAgent.speed;

        // Tạo sẵn 4 điểm đi tuần hình vuông quanh vị trí spawn
        patrolPoints = new Vector3[4];
        patrolPoints[0] = startPosition + new Vector3(patrolRange, 0, patrolRange);
        patrolPoints[1] = startPosition + new Vector3(-patrolRange, 0, patrolRange);
        patrolPoints[2] = startPosition + new Vector3(-patrolRange, 0, -patrolRange);
        patrolPoints[3] = startPosition + new Vector3(patrolRange, 0, -patrolRange);
    }

    private void Start()
    {
        StartCoroutine(FOVRoutime());
    }

    private void Update()
    {
        if (saveGameManager.isCharacterSpawned && player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn_Ground_Skeletons"))
        {
            isSpawned = true;
        }
        if (!isSpawned) return;
        if (enemyStats.isDie)
        {
            navAgent.ResetPath();
            animator.SetFloat("Speed", 0);
            return;
        }

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

            if (distanceToTarget <= ditectionRadius &&
                Vector3.Angle(transform.forward, directionToTarget) < agent / 2 &&
                !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else
        {
            canSeePlayer = false;
        }

        animator.SetBool("Ditection", canSeePlayer);
    }

    public void Movement()
    {
        float distance = (player != null) ? Vector3.Distance(transform.position, player.position) : Mathf.Infinity;

        if (!canSeePlayer)
        {
            PatrolSquare();
            return;
        }

        // Chase & attack player
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

        Vector3 targetPoint = patrolPoints[currentPatrolIndex];
        navAgent.speed = originalSpeed;
        navAgent.SetDestination(targetPoint);
        animator.SetFloat("Speed", navAgent.velocity.magnitude);

        if (Vector3.Distance(transform.position, targetPoint) < 1f)
        {
            StartCoroutine(WaitAndGoNext());
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
