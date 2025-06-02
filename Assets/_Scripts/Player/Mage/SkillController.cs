using UnityEngine;
using UnityEngine.ProBuilder;

public class SkillController : MonoBehaviour
{
    public Animator animator;

    public GameObject bulletPrefab;
    public GameObject fireBreathEffect;
    public GameObject aoeBallPrefab;

    public Transform firePoint;

    public float fireBreathCooldown = 3f;
    private float fireBreathTimer = 0f;
    private bool fireBreathOnCooldown = false;
    private bool isUsingFireBreath = false;

    public float skill2Cooldown = 6f;
    public float skill3Cooldown = 11f;
    private float skill2Timer = 0f;
    private float skill3Timer = 0f;

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
            NormalAttack();
        }
        if (Input.GetKeyDown(KeyCode.E) && !fireBreathOnCooldown)
        {
            StartFireBreath();
        }

        if (Input.GetKeyUp(KeyCode.E) && isUsingFireBreath)
        {
            StopFireBreath();
        }
        if (Input.GetKeyDown(KeyCode.R) && skill2Timer >= skill2Cooldown)
        {
            UseAOEBall();
        }

        if (Input.GetKeyDown(KeyCode.T) && skill3Timer >= skill3Cooldown)
        {
            UseTripleAOE();
        }
    }

    void NormalAttack()
    {
        animator.SetTrigger("Attack1");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Projectile>().Initialize(20f);
    }

    void StartFireBreath()
    {
        animator.SetTrigger("Attack1");
        fireBreathEffect.SetActive(true);
        isUsingFireBreath = true;
    }

    void StopFireBreath()
    {
        fireBreathEffect.SetActive(false);
        isUsingFireBreath = false;
        fireBreathOnCooldown = true;
        fireBreathTimer = 0f;
    }

    void UseAOEBall()
    {
        animator.SetTrigger("Attack2");
        Instantiate(aoeBallPrefab, firePoint.position, firePoint.rotation);
        skill2Timer = 0f;
    }

    void UseTripleAOE()
    {
        animator.SetTrigger("Attack2");
        Instantiate(aoeBallPrefab, firePoint.position + Vector3.left, firePoint.rotation);
        Instantiate(aoeBallPrefab, firePoint.position, firePoint.rotation);
        Instantiate(aoeBallPrefab, firePoint.position + Vector3.right, firePoint.rotation);
        skill3Timer = 0f;
    }
}
