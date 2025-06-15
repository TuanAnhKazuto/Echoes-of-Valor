using UnityEngine;
using UnityEngine.AI;

public class SkeletonNecromancer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float radius = 10f;
    public float maxDistance = 50f;
    public Animator animator;

    public float attackRange = 2f;       // Khoảng cách giữ khi tấn công
    public float rotationSpeed = 5f;     // Tốc độ quay mặt
    public float attackCooldown = 2f;    // Thời gian hồi chiêu
    private float lastAttackTime;

    private Transform target;
    private Vector3 originalePosition;

    public enum CharacterState
    {
        Normal,
        Attack
    }
    public CharacterState currentState;

    void Start()
    {
        // Tự động tìm Player
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }

        originalePosition = transform.position;
    }

    void Update()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        float distanceToOrigin = Vector3.Distance(originalePosition, transform.position);

        if (distanceToTarget <= radius && distanceToOrigin <= maxDistance)
        {
            if (distanceToTarget > attackRange)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.position);
                animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
                ChangeState(CharacterState.Normal);
            }
            else
            {
                navMeshAgent.isStopped = true;
                animator.SetFloat("Speed", 0);

                RotateTowardsTarget();

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    ChangeState(CharacterState.Attack);
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(originalePosition);
            float distanceBack = Vector3.Distance(originalePosition, transform.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            if (distanceBack < 1f)
            {
                animator.SetFloat("Speed", 0);
            }

            ChangeState(CharacterState.Normal);
        }
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

        // Chỉ ngăn đổi trạng thái nếu KHÔNG phải Attack
        if (currentState == newState && newState != CharacterState.Attack)
            return;

        if (newState == CharacterState.Attack)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        currentState = newState;
    }
}
