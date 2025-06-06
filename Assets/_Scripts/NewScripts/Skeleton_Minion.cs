using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Minion : BaseCharacter
{
    private float lastAttackTime;

    protected override void Start()
    {
        base.Start();
        animator.SetTrigger("IsSpawm");
        animator.speed = 0;
    }
    protected override void Update()
    {
        base.Update();

        if (player == null) return;
        if (isAttacking) return;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackTriggerRange)
        {
            if (!isSpawm)
            {
                animator.speed = 1;
                return;
            }
            if (distance > attackRange)
            {
                MoveTowardsPlayer();

                if (animator != null)
                {
                    animator.SetFloat("Speed", moveSpeed);
                    //animator.SetBool("IsAttack", false);
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetFloat("Speed", 0f);
                    isAttacking = true;
                    animator.SetTrigger("IsAttacking");
                    //animator.SetBool("IsAttack", true);
                }
            }
        }
        else
        {
            if (animator != null)
            {
                if (!isSpawm) return;
                animator.SetFloat("Speed", 0f);
                //animator.SetBool("IsAttack", false);
            }
        }

    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
        }
    }

    public override void Attack()
    {
        // Tính khoảng cách giữa enemy và player
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Nếu khoảng cách nhỏ hơn attackRange => gây sát thương
        if (distance < attackRange)
        {
            // Gọi hàm TakeDamage từ player, truyền vào lượng sát thương
            player.TakeDamage(damage);
        }
        else
        {
            // Nếu player ngoài phạm vi tấn công, ghi log "Miss"
            Debug.Log("Miss");
        }
    }



    public override void TakeDamage(int damage)
    {

    }
    public override void Death()
    {

    }
}
