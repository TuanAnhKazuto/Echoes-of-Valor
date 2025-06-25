using UnityEngine;

public class SkeletonMinion : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

    private float nextAttackTime = 0f;

    private Transform player;
    private MonoBehaviour playerHealthScript;

    void Update()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                playerHealthScript = playerObj.GetComponent("PlayerHealth") as MonoBehaviour;
            }
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        if (playerHealthScript != null)
        {
            playerHealthScript.Invoke("TakeDamage", damage);
            Debug.Log("SkeletonMinion tấn công Player, gây " + damage + " sát thương");
        }
    }
}
