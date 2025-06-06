using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    public override void Attack()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        Debug.Log("Player Hit -" + damage);
        if (health.currentHealth <= 0)
        {
            Death();
        }
    }
    public override void Death()
    {
        Debug.Log("Death");
    }
}
