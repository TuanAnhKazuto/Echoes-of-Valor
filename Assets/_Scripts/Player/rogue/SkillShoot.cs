using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillShoot : MonoBehaviour
{
    public GameObject normalArrowPrefab;
    public GameObject fireArrowPrefab;
    public Transform shootPoint;
    public float shootForce = 20f;
    public LayerMask enemyLayer;

    public ParticleSystem fireArrowEffectPrefab;
    public ParticleSystem aoeImpactEffectPrefab;

    private float normalArrowDamage = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FireArrow();
        }
        else if (Input.GetKeyDown(KeyCode.W))
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
        GameObject arrow = Instantiate(fireArrowPrefab, shootPoint.position, shootPoint.rotation);
        arrow.GetComponent<Rigidbody>().linearVelocity = shootPoint.forward * shootForce;
        arrow.GetComponent<ArrowDamage>().damage = normalArrowDamage * 1.75f;

        if (fireArrowEffectPrefab)
        {
            ParticleSystem effect = Instantiate(fireArrowEffectPrefab, arrow.transform.position, Quaternion.identity, arrow.transform);
            effect.Play();
        }
    }

    void DoubleArrow()
    {
        float offset = 0.3f;
        Vector3 leftPos = shootPoint.position - shootPoint.right * offset;
        Vector3 rightPos = shootPoint.position + shootPoint.right * offset;

        GameObject leftArrow = Instantiate(normalArrowPrefab, leftPos, shootPoint.rotation);
        GameObject rightArrow = Instantiate(normalArrowPrefab, rightPos, shootPoint.rotation);

        leftArrow.GetComponent<Rigidbody>().linearVelocity = shootPoint.forward * shootForce;
        rightArrow.GetComponent<Rigidbody>().linearVelocity = shootPoint.forward * shootForce;

        leftArrow.GetComponent<ArrowDamage>().damage = normalArrowDamage * 3f;
        rightArrow.GetComponent<ArrowDamage>().damage = normalArrowDamage * 3f;
    }

    IEnumerator ArrowRain()
    {
        Transform targetEnemy = FindNearestEnemy();
        if (targetEnemy == null) yield break;

        int totalArrows = Random.Range(20, 31);
        int fireCount = Mathf.RoundToInt(totalArrows * 2f / 3f);
        int normalCount = totalArrows - fireCount;

        Vector3 center = targetEnemy.position;

        for (int i = 0; i < totalArrows; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            Vector3 spawnPos = center + new Vector3(Random.Range(-2f, 2f), 10f, Random.Range(-2f, 2f));
            GameObject arrow;
            float damage;

            if (i < fireCount)
            {
                arrow = Instantiate(fireArrowPrefab, spawnPos, Quaternion.identity);
                damage = 35f;

                if (fireArrowEffectPrefab)
                {
                    ParticleSystem effect = Instantiate(fireArrowEffectPrefab, arrow.transform.position, Quaternion.identity, arrow.transform);
                    effect.Play();
                }
            }
            else
            {
                arrow = Instantiate(normalArrowPrefab, spawnPos, Quaternion.identity);
                damage = 20f;
            }

            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.down * shootForce;
            arrow.GetComponent<ArrowDamage>().damage = damage;

            arrow.GetComponent<ArrowDamage>().onAOEImpactEffect = aoeImpactEffectPrefab;
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

// Attach this to your arrow prefab
public class ArrowDamage : MonoBehaviour
{
    public float damage = 20f;
    public ParticleSystem onAOEImpactEffect;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Apply damage to enemy here
            Debug.Log($"Dealt {damage} damage to {collision.gameObject.name}");

            if (onAOEImpactEffect != null)
            {
                Instantiate(onAOEImpactEffect, transform.position, Quaternion.identity).Play();
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
