using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseCharacter : MonoBehaviour
{
    [Header("Target & Attack Type")]
    public BaseCharacter target;                 // Đối tượng mục tiêu để tấn công
    public AttackType attackType = AttackType.Melee;  // Kiểu tấn công (Cận chiến hoặc Tầm xa)

    [Header("Movement & Attack Settings")]
    public float moveSpeed = 3f;                  // Tốc độ di chuyển của nhân vật
    public float attackTriggerRange = 10f;       // Khoảng cách bắt đầu chuyển sang trạng thái tấn công (dừng di chuyển, bật animation tấn công)
    public float attackRange = 2f;                // Khoảng cách thực hiện gây sát thương
    public float attackCooldown = 1.5f;           // Thời gian chờ giữa các đòn tấn công liên tiếp
    public int damage = 10;                        // Lượng sát thương gây ra mỗi đòn

    [Header("Internal Components")]
    public HealthController healthController { get; private set; } // Tham chiếu đến HealthController của nhân vật
    protected Animator animator;                  // Tham chiếu Animator để điều khiển animation
    protected AnimationType currentAnimation = AnimationType.Idle; // Trạng thái animation hiện tại

    [Header("NavMesh Settings")]
    protected NavMeshAgent agent;

    [Header("Gizmos Settings")]
    [Tooltip("Hiển thị vùng phát hiện tấn công (Attack Trigger Range)")]
    public bool showAttackTriggerGizmo = true;
    [Tooltip("Hiển thị vùng gây sát thương (Attack Range)")]
    public bool showAttackRangeGizmo = true;


    protected bool IsSpawm = false;
    protected bool IsDead
    {
        get
        {
            if (healthController != null)
                return healthController.IsDead;
            return false;
        }
    }
    protected bool IsAttacking = false;


    protected float distanceToTarget;
    protected virtual void Awake()
    {
        healthController = GetComponent<HealthController>();
        if (healthController == null) healthController = gameObject.AddComponent<HealthController>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        // Trạng thái animation ban đầu là Spawm sau đó dừng lại luôn.
        currentAnimation = AnimationType.Spawn;
        if (animator)
        {
            animator.SetTrigger("Spawm");
            animator.speed = 0;
        }
    }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Death();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(damage);
        }
    }
    public void OnAnimationStart(AnimationType type)
    {
        this.currentAnimation = type;
        
        switch (type)
        {
            case AnimationType.Attack:
                IsAttacking = true;
                break;
        }
    }

    public void OnAnimationEvent(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Attack:
                Attack();
                break;
        }
    }

    public void OnAnimationEnd(AnimationType type)
    {

        switch (type)
        {
            case AnimationType.Attack:
                IsAttacking = false;
                break;
            case AnimationType.Spawn:
                IsSpawm = true;
                break;
        }
    }
    protected bool IsFacingTarget()
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        directionToTarget.y = 0;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        return angle < 10f;
    }
    protected void RotateToFaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }


    public virtual void Attack()
    {
        //float distance = Vector3.Distance(transform.position, target.transform.position);
        //if (distance <= attackRange)
        //{
        //    target?.TakeDamage(damage);
        //} else
        //{
        //    Debug.Log("Miss");
        //}
    }
    public virtual void TakeDamage(int damage)
    {
        healthController.TakeDamage(damage,this);
    }
    public virtual void Death()
    {
        animator.SetTrigger("Death");
    }
    protected virtual void OnDrawGizmos()
    {
        if (showAttackTriggerGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackTriggerRange);
        }

        if (showAttackRangeGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
