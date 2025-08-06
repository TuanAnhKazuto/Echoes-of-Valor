using UnityEngine;

public class Knight : MonoBehaviour
{
    public PlayerController player;
    public CharacterStats characterStats;
    Animator anim;

    public Transform currentTarget;
    public float curTargetRange;
    public float normalAttackRange = 3.5f;
    public float skillAttackRange = 8f;

    public float coolDownTime = 0.8f;
    private float nextAttackTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0f;
    float maxComboDelay = 1f;

    public string[] animAtkName = new string[] { "Attack1", "Attack2", "Attack3", "Attack4", "Attack5" };
    public HitBoxController hitBox;

    public SwordTrailController trailEffect;

    private void Start()
    {
        anim = player.animator;
        player.isAttacking = false;
        curTargetRange = normalAttackRange;

        if (trailEffect != null)
        {
            trailEffect.StopTrail();
        }
    }

    private void Update()
    {
        Attack();
    }

    public void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);

            if (distance < closestDistance && distance <= curTargetRange)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }

        currentTarget = closest;

        if (currentTarget != null)
        {
            // Xoay nhân vật hướng về mục tiêu
            Vector3 direction = (currentTarget.position - player.transform.position).normalized;
            direction.y = 0; // bỏ trục Y nếu game 3D
            player.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void Attack()
    {
        if (player.isTalkingWithNPC) return;

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("IsAttack", true);
            FindClosestEnemy();
            player.canMove = false;
        }
        else
        {
            for(int i = 0; i <= 4; i++)
            {
                //Debug.Log($"Checking animation state: {animAtkName[i]}");
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(animAtkName[i]) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    anim.SetBool("IsAttack", false);
                    hitBox.EndAttack();
                    player.canMove = true;
                }
            }
        }
    }

    void AttackOld()
    {
        // Old version of the code
        if (player.isTalkingWithNPC) return;
        if (player.isDashing)
        {
            player.isAttacking = false;
            anim.SetBool("IsAttack1", false);
            anim.SetBool("IsAttack2", false);
            anim.SetBool("IsAttack3", false);
            anim.SetBool("IsAttack4", false);
            anim.SetBool("IsAttack5", false);
        }

        if (player.isJumping || player.isDashing) return;

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            anim.SetBool("IsAttack1", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            anim.SetBool("IsAttack2", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            anim.SetBool("IsAttack3", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
        {
            anim.SetBool("IsAttack4", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack5"))
        {
            anim.SetBool("IsAttack5", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
            player.isAttacking = false;
        }
        if (Time.time > nextAttackTime && CanAcceptInput())
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindClosestEnemy();
                OnClick();
                player.isAttacking = true;
            }
        }
    }

    bool CanAcceptInput()
    {
        var state = anim.GetCurrentAnimatorStateInfo(0);
        return state.IsName("Attack1") || state.IsName("Attack2") || state.IsName("Attack3") ||
               state.IsName("Attack4") || state.IsName("Attack5") || state.IsName("Idle") || state.IsName("Movement");
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.SetBool("IsAttack1", true);
            PlaySwordTrail();
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 5);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            anim.SetBool("IsAttack1", false);
            anim.SetBool("IsAttack2", true);
            PlaySwordTrail();
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            anim.SetBool("IsAttack2", false);
            anim.SetBool("IsAttack3", true);
            PlaySwordTrail();
        }
        if (noOfClicks >= 4 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            anim.SetBool("IsAttack3", false);
            anim.SetBool("IsAttack4", true);
            PlaySwordTrail();
        }
        if (noOfClicks >= 5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
        {
            anim.SetBool("IsAttack4", false);
            anim.SetBool("IsAttack5", true);
        }
    }
    void PlaySwordTrail()
    {
        if (trailEffect != null)
        {
            trailEffect.StartTrail();
            Invoke("StopTrail", 0.3f); 
        }
    }

    void StopTrail()
    {
        if (trailEffect != null)
        {
            trailEffect.StopTrail();
        }
    }
}
