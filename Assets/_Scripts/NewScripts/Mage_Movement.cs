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
        while (Vector3.Distance(bullet.position, target.position) > 0.1f)
        {
            bullet.position = Vector3.MoveTowards(
                bullet.position,
                target.position + new Vector3(0, 1.5f, 0),
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
