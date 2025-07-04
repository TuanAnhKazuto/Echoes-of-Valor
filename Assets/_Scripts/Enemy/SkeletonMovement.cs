using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMovement : MonoBehaviour
{
    public float radiusLookAt = 10f;
    public float attackRange = 8f;
    private float originalSpeed = 6f;

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
        float distance = Vector3.Distance(transform.position, player.position);

        if (!canSeePlayer)
        {
            navAgent.SetDestination(startTransform);
            animator.SetFloat("Speed", navAgent.velocity.magnitude);
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
}
