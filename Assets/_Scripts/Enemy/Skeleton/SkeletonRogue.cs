using UnityEngine;
using UnityEngine.AI;

public class SkeletonRogue : MonoBehaviour
{
    public float radiusLookAt = 10f;
    public float attackRange = 8f;
    private float originalSpeed = 6f;

    [SerializeField] private bool isSpawned = false;

    NavMeshAgent agent;
    Transform player;
    Animator animator;

    public GameObject arrowPrefab;
    public Transform firePoint;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn_Ground_Skeletons"))
        {
            isSpawned = true;
        }
        if (!isSpawned) return;
        Movement();    
    }

    private void Movement()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= radiusLookAt)
        {
            agent.SetDestination(player.position);
            animator.SetFloat("Speed", agent.velocity.magnitude);

            if(distance <= attackRange)
            {
                animator.SetBool("IsAttack", true);
                agent.speed = 0f;
            }
            else
            {
                animator.SetBool("IsAttack", false);
                agent.speed = originalSpeed;
            }
        }
    }

    public void Attack()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.transform.position, firePoint.rotation);
        //arrow.gameObject.transform.Rotate(-90, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusLookAt);
    }

    
}
