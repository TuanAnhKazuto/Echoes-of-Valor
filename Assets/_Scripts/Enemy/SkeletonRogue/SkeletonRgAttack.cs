using UnityEngine;

public class SkeletonRgAttack : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;

    public void Attack()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.transform.position, firePoint.rotation);
        //arrow.gameObject.transform.Rotate(-90, 0, 0);
    }
}
