using System.Collections;
using UnityEngine;

public class Mage_Movement : EnemyMovementManual
{
    public GameObject bullet;
    public float speed = 10f;

    public override void SpawnBullet(CharacterStats characterStats)
    {
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        StartCoroutine(IEShoot(characterStats.transform, bulletClone.transform));
    }

    private IEnumerator IEShoot(Transform target, Transform bullet)
    {
        Vector3 targetPos = target.position + new Vector3(0, 1.5f, 0); // CHỐT 1 LẦN

        while (bullet != null && Vector3.Distance(bullet.position, targetPos) > 0.1f)
        {
            bullet.position = Vector3.MoveTowards(
                bullet.position,
                targetPos,
                speed * Time.deltaTime
            );

            yield return null;
        }

        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

}
