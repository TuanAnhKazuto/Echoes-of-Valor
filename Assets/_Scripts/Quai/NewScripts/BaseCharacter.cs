using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [Header("Cài đặt AI")]
    public Player player;
    public float moveSpeed = 3f;
    public float attackTriggerRange = 10f;  // Vùng bắt đầu chuyển sang trạng thái Attack (animation, dừng di chuyển)
    public float attackRange = 2f;          // Khoảng cách để thực hiện gây sát thương
    public float attackCooldown = 1.5f;
    public int damage = 10;


    public HEALTH health {  get; private set; }
    protected Animator animator;

    protected bool isSpawm = false;
    protected bool isAttacking = false;
    protected AnimationType currentAnimation = AnimationType.Idle;


    protected virtual void ResetState()
    {
        isSpawm = false;
        isAttacking = false;
        currentAnimation = AnimationType.Idle;
    }
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<HEALTH>();
    }
    protected virtual void Start()
    {
       
    }
    protected virtual void Update() {
        //if (player == null) return;
        //if (isAttacking) return;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackTriggerRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public abstract void Attack();
    public abstract void TakeDamage(int damage);
    public abstract void Death();

    public virtual void OnAniStart(AnimationType type)
    {
        this.currentAnimation = type;

        switch (currentAnimation)
        {
            case AnimationType.Idle:
                isAttacking = false;
                break;
            case AnimationType.Attack:
                isAttacking = true;

                break;
            case AnimationType.Move:
                isAttacking = false;
                break;
            case AnimationType.Death:
                isAttacking = false;
                break;
        }
    }
    public virtual void OnAniEvent()
    {
        switch (currentAnimation)
        {
            case AnimationType.Idle:
                break;
            case AnimationType.Attack:
                Attack();
                break;
            case AnimationType.Move:
                break;
            case AnimationType.Death:
                break;
        }
    }
    public virtual void OnAniEnd()
    {
        switch (currentAnimation)
        {
            case AnimationType.Idle:
                break;
            case AnimationType.Attack:
                isAttacking = false;
                break;
            case AnimationType.Move:
                break;
            case AnimationType.Death:
                break;
            case AnimationType.Spawm:
                isSpawm = true;
                break;
        }
    }

}


public enum AnimationType
{
    Idle,
    Attack,
    Move,
    Death,
    Spawm
}