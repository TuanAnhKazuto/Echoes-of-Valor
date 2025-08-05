using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    private Animator animator;
    private CharacterStats characterStats;
    public Transform characterTransform;

    [Header("Skill 1 Fire Breath")]
    public GameObject fireBreathEffect;
    public float fireBreathCooldown = 3f;
    private float fireBreathTimer = 0f;
    private bool fireBreathOnCooldown = false;
    private bool isUsingFireBreath = false;
    public float fireBreathManaCost = 10f;

    [Header("Skill 2 Big Ball")]
    public GameObject BigBallPrefab;
    public float bigBallManaCost = 80f;

    [Header("Skill 3 Lightning Cloud")]
    public GameObject cloudObject;
    public Transform cloudPoint;
    public GameObject lightningVFXPrefab;
    public float cloudDetectionRadius = 20f;
    public float heightAboveEnemy = 5f;
    public float lightningInterval = 1f;
    private bool isCloudAttacking = false;
    public float lightningCloudManaCost = 25f;

    [Header("Common")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float skill2Cooldown = 6f;
    public float skill3Cooldown = 6f;
    private float skill2Timer = 0f;
    private float skill3Timer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterStats = GetComponentInParent<CharacterStats>();
    }

    void Update()
    {
        if (fireBreathOnCooldown)
            fireBreathTimer += Time.deltaTime;

        if (fireBreathOnCooldown && fireBreathTimer >= fireBreathCooldown)
        {
            fireBreathOnCooldown = false;
            fireBreathTimer = 0f;
        }

        skill2Timer += Time.deltaTime;
        skill3Timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SkillTarget(15f, (enemy) =>
            {
                if (enemy != null)
                {
                    LookAtTarget(enemy);
                }
                animator.SetTrigger("Attack");
            }));
        }

        // Fire Breath
        if (Input.GetKey(KeyCode.Alpha1) && !fireBreathOnCooldown && !isUsingFireBreath)
        {
            animator.SetBool("Skill1", true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1) && isUsingFireBreath)
        {
            StopFireBreath();
        }

        // Big Ball
        if (Input.GetKeyDown(KeyCode.Alpha2) && skill2Timer >= skill2Cooldown)
        {
            animator.SetTrigger("Attack2");
        }

        // Lightning Cloud
        if (Input.GetKeyDown(KeyCode.Alpha3) && skill3Timer >= skill3Cooldown && !isCloudAttacking)
        {
            StartCoroutine(SkillTarget(cloudDetectionRadius, (enemy) =>
            {
                StartCoroutine(ExecuteLightningSkill(enemy));
            }));
        }
    }

    void NormalAttack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Projectile>().Initialize(20f);
    }

    // Gọi từ Animation Event
    void StartFireBreath()
    {
        if (!characterStats.ConsumeMana(fireBreathManaCost)) return;

        fireBreathEffect.SetActive(true);
        isUsingFireBreath = true;
    }

    void StopFireBreath()
    {
        fireBreathEffect.SetActive(false);
        animator.SetBool("Skill1", false);
        isUsingFireBreath = false;
        fireBreathOnCooldown = true;
        fireBreathTimer = 0f;
    }

    // Gọi từ Animation Event
    void UseAOEBall()
    {
        if (!characterStats.ConsumeMana(bigBallManaCost)) return;

        Instantiate(BigBallPrefab, firePoint.position, firePoint.rotation);
        skill2Timer = 0f;
    }

    IEnumerator ExecuteLightningSkill(Transform enemy)
    {
        if (!characterStats.ConsumeMana(lightningCloudManaCost))
            yield break;

        isCloudAttacking = true;
        skill3Timer = 0f;

        cloudObject.SetActive(true);
        cloudObject.transform.localScale = Vector3.one;

        if (enemy == null)
        {
            cloudObject.transform.position = cloudPoint.position + Vector3.up * 5f;
            yield return new WaitForSeconds(3f);
            cloudObject.SetActive(false);
            isCloudAttacking = false;
            yield break;
        }

        Vector3 aboveTarget = enemy.position + Vector3.up * heightAboveEnemy;
        cloudObject.transform.position = aboveTarget;

        for (int i = 0; i < 5; i++)
        {
            GameObject vfx = Instantiate(lightningVFXPrefab, cloudObject.transform.position, Quaternion.identity);

            Collider[] hits = Physics.OverlapSphere(cloudObject.transform.position, 5f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
                {
                    var stats = hit.GetComponent<EnemyStats>();
                    if (stats != null)
                    {
                        stats.TakeDamage(40f);
                        Vector3 popupPos = hit.transform.position + Vector3.up * 2f;
                        DamagePopupSpawner.Instance.ShowDamage(popupPos, 40, Color.red);
                    }
                }
            }

            Destroy(vfx, 2f);
            yield return new WaitForSeconds(lightningInterval);
        }

        cloudObject.SetActive(false);
        isCloudAttacking = false;
    }

    IEnumerator SkillTarget(float radius, System.Action<Transform> onTargetFound)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        Transform nearestEnemy = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestEnemy = hit.transform;
                }
            }
        }

        yield return null;
        onTargetFound?.Invoke(nearestEnemy);
    }

    void LookAtTarget(Transform target)
    {
        if (characterTransform == null || target == null) return;

        Vector3 direction = (target.position - characterTransform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            characterTransform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
