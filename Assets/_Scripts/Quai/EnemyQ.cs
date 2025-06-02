using UnityEngine;
using UnityEngine.AI;

public class EnemyQ : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public Transform player;

    private int currentPoint = 0;
    private Animator animator;
    private NavMeshAgent agent;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Update Speed parameter based on agent velocity
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        animator.SetBool("attack", false);
        agent.stoppingDistance = 0f;

        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("attack", false);
        agent.stoppingDistance = attackRange - 0.1f;
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.ResetPath(); 
        transform.LookAt(player); 
        animator.SetBool("attack", true);
    }
}
