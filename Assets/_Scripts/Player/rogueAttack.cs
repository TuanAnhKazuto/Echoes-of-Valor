using UnityEngine;

public class rogueAttack : MonoBehaviour
{
    public Animator animator;

    public PlayerController playerController;
    private bool isAttacking = false;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on object.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        if (playerController != null)
        {
            playerController.isAttacking = true;
        }
        animator.SetTrigger("Attack");
    }

    // Hàm này nên được gọi bởi Animation Event ở cuối animation Attack
    public void EndAttack()
    {
        isAttacking = false;
        if (playerController != null)
        {
            playerController.isAttacking = false;
        }
    }
}
