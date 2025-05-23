using UnityEngine;
using UnityEngine.AI;

public class Quaidichuyen : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
            animator.SetBool("isRunning", true);

            if (distance <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("isRunning", false);
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("attack");

            // Gây sát thương nếu cần
            HEALTH HEALTH = player.GetComponent<HEALTH>();
            if (HEALTH != null)
            {
                HEALTH.TakeDamage(damage);
            }
        }
    }
}