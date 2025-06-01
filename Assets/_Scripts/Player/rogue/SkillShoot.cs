using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillShoot : MonoBehaviour
{
    public GameObject normalArrowPrefab;
    public GameObject fireArrowPrefab;
    public Transform shootPoint;
    public float arrowSpeed = 15f;
    public LayerMask enemyLayer;

    public ParticleSystem fireArrowEffectPrefab;
    public ParticleSystem aoeImpactEffectPrefab;

    private float normalArrowDamage = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FireArrow();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            DoubleArrow();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ArrowRain());
        }
    }

    void FireArrow()
    {
        Transform target = FindNearestEnemy();
        GameObject arrow = Instantiate(fireArrowPrefab, shootPoint.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.speed = arrowSpeed;
            arrowScript.SetTarget(target);
        }

        ArrowDamage dmg = arrow.GetComponent<ArrowDamage>();
        if (dmg != null) dmg.damage = normalArrowDamage * 1.75f;

        if (fireArrowEffectPrefab)
        {
            ParticleSystem fireFX = Instantiate(fireArrowEffectPrefab, arrow.transform);
            fireFX.transform.localPosition = Vector3.zero;
        }
    }

    void DoubleArrow()
    {
        Transform target = FindNearestEnemy();
        if (target == null) return;

        float offset = 0.3f;
        Vector3 leftPos = shootPoint.position - shootPoint.right * offset;
        Vector3 rightPos = shootPoint.position + shootPoint.right * offset;

        Vector3 targetPos = target.position + Vector3.up * 1.5f;
        Vector3 direction = (targetPos - shootPoint.position).normalized;

        GameObject leftArrow = Instantiate(normalArrowPrefab, leftPos, Quaternion.identity);
        GameObject rightArrow = Instantiate(normalArrowPrefab, rightPos, Quaternion.identity);

        Arrow leftScript = leftArrow.GetComponent<Arrow>();
        Arrow rightScript = rightArrow.GetComponent<Arrow>();

        if (leftScript != null)
        {
            leftScript.speed = arrowSpeed;
            leftScript.SetDirection(direction);
        }

        if (rightScript != null)
        {
            rightScript.speed = arrowSpeed;
            rightScript.SetDirection(direction);
        }

        ArrowDamage dmgL = leftArrow.GetComponent<ArrowDamage>();
        ArrowDamage dmgR = rightArrow.GetComponent<ArrowDamage>();

        if (dmgL != null) dmgL.damage = normalArrowDamage * 3f;
        if (dmgR != null) dmgR.damage = normalArrowDamage * 3f;
    }

    IEnumerator ArrowRain()
    {
        Transform targetEnemy = FindNearestEnemy();
        if (targetEnemy == null) yield break;

        int totalArrows = Random.Range(20, 31);
        int fireCount = Mathf.RoundToInt(totalArrows * 2f / 3f);
        Vector3 center = targetEnemy.position;

        for (int i = 0; i < totalArrows; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            Vector3 spawnPos = center + new Vector3(Random.Range(-2f, 2f), 10f, Random.Range(-2f, 2f));
            GameObject arrow;
            float damage;

            bool isFire = i < fireCount;

            if (isFire)
            {
                arrow = Instantiate(fireArrowPrefab, spawnPos, Quaternion.identity);
                damage = 35f;

                if (fireArrowEffectPrefab)
                {
                    ParticleSystem fireFX = Instantiate(fireArrowEffectPrefab, arrow.transform);
                    fireFX.transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                arrow = Instantiate(normalArrowPrefab, spawnPos, Quaternion.identity);
                damage = 20f;
            }

            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                arrowScript.speed = arrowSpeed;
                arrowScript.SetDirection(Vector3.down);
            }

            ArrowDamage dmg = arrow.GetComponent<ArrowDamage>();
            if (dmg != null)
            {
                dmg.damage = damage;
                dmg.onAOEImpactEffect = aoeImpactEffectPrefab;
            }
        }
    }

    Transform FindNearestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 20f, enemyLayer);
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var col in enemies)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = col.transform;
            }
        }
        return nearest;
    }
}
