using UnityEngine;
using System.Collections;
using static SkeletonNecromancer;

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

    public float fireArrowManaCost = 17f;
    public float doubleArrowManaCost = 18f;
    public float arrowRainManaCost = 170f;

    private CharacterStats characterStats;

    private bool canUseSkill1 = true;
    private bool canUseSkill2 = true;
    private bool canUseSkill3 = true;

    private float skill1Cooldown = 1f;
    private float skill2Cooldown = 1.5f;
    private float skill3Cooldown = 5f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        characterStats = GetComponentInParent<CharacterStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canUseSkill1)
        {
            Transform target = FindNearestEnemy(25);
            if (target != null) LookAtTarget(target);

            if (characterStats != null && characterStats.currentMana >= fireArrowManaCost)
            {
                if (animator) animator.SetTrigger("Attack3");
                StartCoroutine(CastSkill1());
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseSkill2)
        {
            Transform target = FindNearestEnemy(25);
            if (target != null) LookAtTarget(target);

            if (characterStats != null && characterStats.currentMana >= doubleArrowManaCost)
            {
                if (animator) animator.SetTrigger("Attack2");
                StartCoroutine(CastSkill2());
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && canUseSkill3)
        {
            Transform target = FindNearestEnemy(25);
            if (target != null) LookAtTarget(target);

            if (characterStats != null && characterStats.currentMana >= arrowRainManaCost)
            {
                StartCoroutine(CastSkill3());
            }
        }
    }

    IEnumerator CastSkill1()
    {
        canUseSkill1 = false;
        yield return new WaitForSeconds(0.2f);

        if (characterStats.ConsumeMana(fireArrowManaCost))
        {
            FireArrow();
        }

        yield return new WaitForSeconds(skill1Cooldown);
        canUseSkill1 = true;
    }

    IEnumerator CastSkill2()
    {
        canUseSkill2 = false;
        yield return new WaitForSeconds(0.2f);

        if (characterStats.ConsumeMana(doubleArrowManaCost))
        {
            DoubleArrow();
        }

        yield return new WaitForSeconds(skill2Cooldown);
        canUseSkill2 = true;
    }

    IEnumerator CastSkill3()
    {
        canUseSkill3 = false;

        if (characterStats.ConsumeMana(arrowRainManaCost))
        {
            yield return ArrowRain();
        }

        yield return new WaitForSeconds(skill3Cooldown);
        canUseSkill3 = true;
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

    Transform FindNearestEnemy(float range = 20f)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemyLayer);
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

    void LookAtTarget(Transform target)
    {
        Transform playerTransform = transform.root;

        Vector3 direction = (target.position - playerTransform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = lookRotation;
        }
    }
}
