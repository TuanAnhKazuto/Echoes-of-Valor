using UnityEngine;

public class rogueAttack : MonoBehaviour
{
    public Animator animator;

    public PlayerController playerController;
    private bool isAttacking = false;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
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
    }
    public void EndAttack()
    {
        isAttacking = false;
        if (playerController != null)
        {
            playerController.isAttacking = false;
        }
    }
}
