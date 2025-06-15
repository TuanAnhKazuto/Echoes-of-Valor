using UnityEngine;
using System.Collections;

public class SkillShoot : MonoBehaviour
{
    private Animator animator;

    public GameObject normalArrowPrefab;
    public GameObject fireArrowPrefab;

    public Transform shootPoint;
    public float arrowSpeed = 20f;
    public LayerMask enemyLayer;

    private float normalArrowDamage = 20f;

    public ParticleSystem fireArrowEffectPrefab;
    public ParticleSystem aoeImpactEffectPrefab;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (animator) animator.SetTrigger("Attack3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (animator) animator.SetTrigger("Attack2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(ArrowRain());
        }
    }

    void FireArrow()
    {
        Transform target = FindNearestEnemy();
        if (target == null || shootPoint == null || fireArrowPrefab == null) return;

        GameObject arrow = Instantiate(fireArrowPrefab, shootPoint.position, Quaternion.identity);

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.speed = arrowSpeed;
            arrowScript.SetTarget(target);
        }

        ArrowDamage dmg = arrow.GetComponent<ArrowDamage>();
        if (dmg != null)
        {
            dmg.damage = normalArrowDamage * 1.75f;
        }

        if (fireArrowEffectPrefab != null)
        {
            ParticleSystem fireFX = Instantiate(fireArrowEffectPrefab, arrow.transform);
            fireFX.transform.localPosition = Vector3.zero;
        }
    }

    void DoubleArrow()
    {
        Transform target = FindNearestEnemy();
        if (target == null || shootPoint == null || normalArrowPrefab == null) return;

        float offset = 0.1f;
        Vector3 leftPos = shootPoint.position - shootPoint.right * offset;
        Vector3 rightPos = shootPoint.position + shootPoint.right * offset;

        Vector3 targetPos = target.position + Vector3.up * 1.5f;
        Vector3 dir = (targetPos - shootPoint.position).normalized;

        GameObject leftArrow = Instantiate(normalArrowPrefab, leftPos, Quaternion.LookRotation(dir));
        Arrow arrowScriptL = leftArrow.GetComponent<Arrow>();
        if (arrowScriptL != null)
        {
            arrowScriptL.speed = arrowSpeed;
            arrowScriptL.SetTarget(target);
        }
        ArrowDamage dmgL = leftArrow.GetComponent<ArrowDamage>();
        if (dmgL != null)
        {
            dmgL.damage = normalArrowDamage * 3f;
        }

        GameObject rightArrow = Instantiate(normalArrowPrefab, rightPos, Quaternion.LookRotation(dir));
        Arrow arrowScriptR = rightArrow.GetComponent<Arrow>();
        if (arrowScriptR != null)
        {
            arrowScriptR.speed = arrowSpeed;
            arrowScriptR.SetTarget(target); 
        }

        ArrowDamage dmgR = rightArrow.GetComponent<ArrowDamage>();
        if (dmgR != null)
        {
            dmgR.damage = normalArrowDamage * 3f;
        }
    }


    IEnumerator ArrowRain()
    {
        Transform target = FindNearestEnemy();
        if (target == null || normalArrowPrefab == null || fireArrowPrefab == null) yield break;

        int totalArrows = Random.Range(20, 31);
        int fireCount = Mathf.RoundToInt(totalArrows * 2f / 3f);
        Vector3 center = target.position;

        for (int i = 0; i < totalArrows; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            Vector3 spawnPos = center + new Vector3(Random.Range(-2f, 2f), 10f, Random.Range(-2f, 2f));
            Vector3 dir = Vector3.down;

            bool isFire = i < fireCount;
            GameObject prefab = isFire ? fireArrowPrefab : normalArrowPrefab;
            float damage = isFire ? 35f : 20f;

            GameObject arrow = Instantiate(prefab, spawnPos, Quaternion.identity);

            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                arrowScript.speed = arrowSpeed;
                arrowScript.SetDirection(dir);
            }

            ArrowDamage dmg = arrow.GetComponent<ArrowDamage>();
            if (dmg != null)
            {
                dmg.damage = damage;
                dmg.onAOEImpactEffect = aoeImpactEffectPrefab;
            }

            if (isFire && fireArrowEffectPrefab != null)
            {
                ParticleSystem fireFX = Instantiate(fireArrowEffectPrefab, arrow.transform);
                fireFX.transform.localPosition = Vector3.zero;
            }
        }
    }

    void ShootArrow(GameObject prefab, Vector3 position, Vector3 direction, float damage)
    {
        if (prefab == null) return;

        GameObject arrow = Instantiate(prefab, position, Quaternion.LookRotation(direction));

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.speed = arrowSpeed;
            arrowScript.SetDirection(direction);
        }

        ArrowDamage dmg = arrow.GetComponent<ArrowDamage>();
        if (dmg != null)
        {
            dmg.damage = damage;
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
