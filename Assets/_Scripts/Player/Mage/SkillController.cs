using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    private Animator animator;

    [Header("Skill 1 Fire Breath")]
    public GameObject fireBreathEffect;
    public float fireBreathCooldown = 3f;
    private float fireBreathTimer = 0f;
    private bool fireBreathOnCooldown = false;
    private bool isUsingFireBreath = false;

    [Header("Skill 2 Big Ball")]
    public GameObject BigBallPrefab;

    [Header("Skill 3 Lightning Cloud")]
    public GameObject cloudObject;
    public Transform cloudHomePosition;
    public GameObject lightningVFXPrefab;
    public float cloudDetectionRadius = 20f;
    public float heightAboveEnemy = 5f;
    public float lightningInterval = 1f;
    private bool isCloudAttacking = false;

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
                    Vector3 dir = (enemy.position - transform.position).normalized;
                    dir.y = 0f;
                    if (dir != Vector3.zero)
                        transform.rotation = Quaternion.LookRotation(dir);
                }

                animator.SetTrigger("Attack");
            }));
        }

        if (Input.GetKey(KeyCode.Alpha1) && !fireBreathOnCooldown)
        {
            animator.SetBool("Skill1", true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1) && isUsingFireBreath)
        {
            StopFireBreath();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && skill2Timer >= skill2Cooldown)
        {
            animator.SetTrigger("Attack2");
        }

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

    void StartFireBreath()
    {
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

    void UseAOEBall()
    {
        Instantiate(BigBallPrefab, firePoint.position, firePoint.rotation);
        skill2Timer = 0f;
    }

    IEnumerator ExecuteLightningSkill(Transform enemy)
    {
        isCloudAttacking = true;
        skill3Timer = 0f;

        // Kích hoạt đám mây
        cloudObject.SetActive(true);

        // Reset scale nếu vô tình bị sai
        cloudObject.transform.localScale = Vector3.one;

        // Khi KHÔNG có enemy
        if (enemy == null)
        {
            cloudObject.transform.position = cloudHomePosition.position + Vector3.up * 5f;
            Debug.Log("No enemy: Cloud stays at home position");

            yield return new WaitForSeconds(3f);
            cloudObject.SetActive(false);
            isCloudAttacking = false;
            yield break;
        }

        // Khi CÓ enemy
        Vector3 aboveTarget = enemy.position + Vector3.up * heightAboveEnemy;
        cloudObject.transform.position = aboveTarget;
        Debug.Log("Cloud moved above enemy: " + enemy.name);

        for (int i = 0; i < 5; i++)
        {
            GameObject vfx = Instantiate(lightningVFXPrefab, cloudObject.transform.position, Quaternion.identity);

            // Gây sát thương xung quanh
            Collider[] hits = Physics.OverlapSphere(cloudObject.transform.position, 2f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
                {
                    hit.GetComponent<Enemy>()?.TakeDamage(40f);
                }
            }

            Destroy(vfx, 2f);
            yield return new WaitForSeconds(lightningInterval);
        }

        yield return new WaitForSeconds(0.5f);
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
}
