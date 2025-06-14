using UnityEngine;
using UnityEngine.ProBuilder;

public class SkillController : MonoBehaviour
{
    private Animator animator;

    public GameObject bulletPrefab;
    public GameObject fireBreathEffect;
    public GameObject BigBallPrefab;

    public Transform firePoint;

    public float fireBreathCooldown = 3f;
    private float fireBreathTimer = 0f;
    private bool fireBreathOnCooldown = false;
    private bool isUsingFireBreath = false;

    public float skill2Cooldown = 6f;
    public float skill3Cooldown = 3f;
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
            animator.SetTrigger("Attack");
        }

        if (Input.GetKey(KeyCode.Alpha1) && !fireBreathOnCooldown)
        {
            animator.SetBool("Skil1", true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha1) && isUsingFireBreath)
        {
            StopFireBreath();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && skill2Timer >= skill2Cooldown)
        {
            animator.SetTrigger("Attack2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && skill3Timer >= skill3Cooldown)
        {
            UseTripleAOE();
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

    void UseTripleAOE()
    {
        animator.SetTrigger("Attack2");
        Instantiate(BigBallPrefab, firePoint.position + Vector3.left, firePoint.rotation);
        skill3Timer = 0f;
    }
}
