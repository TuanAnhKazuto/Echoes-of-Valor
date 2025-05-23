using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    PlayerController playerController;


    void IsAttck()
    {
        playerController.isAttacking = true;
    }
}
