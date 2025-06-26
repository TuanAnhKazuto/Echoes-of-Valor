using System.Threading.Tasks;
using UnityEngine;

public class SkeletonRgAttack : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    
    public SkeletonArrow SkeletonArrow;
    public EnemyStats enemyStats;   

    public async Task Attack()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.transform.position, firePoint.rotation);
        enemyStats = GetComponent<EnemyStats>();
        SkeletonArrow = arrow.GetComponent<SkeletonArrow>();
        SkeletonArrow.damage = enemyStats.baseDamage;
    }
}
