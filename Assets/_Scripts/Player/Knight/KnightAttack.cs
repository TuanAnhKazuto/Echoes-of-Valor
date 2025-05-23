using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    public PlayerController player;
    public GameObject hitBox;
    Animator anim;

    public float coolDownTime = 0.8f;
    private float nextAttackTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0f;
    float maxComboDelay = 2f;

    private void Start()
    {
        anim = player.animator;
    }

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        player.isAttacking = true;

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
        }
        if (Time.time > nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.SetBool("IsAttack1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 5);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            anim.SetBool("IsAttack1", false);
            anim.SetBool("IsAttack2", true);
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            anim.SetBool("IsAttack2", false);
            anim.SetBool("IsAttack3", true);
        }
        if (noOfClicks >= 4 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            anim.SetBool("IsAttack3", false);
            anim.SetBool("IsAttack4", true);
        }
        if (noOfClicks >= 5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
        {
            anim.SetBool("IsAttack4", false);
            anim.SetBool("IsAttack5", true);
        }
    }

    public void OnAttack()
    {
        hitBox.SetActive(true);
    }

    public void EndAttack()
    {
        hitBox.SetActive(false);
    }
}
