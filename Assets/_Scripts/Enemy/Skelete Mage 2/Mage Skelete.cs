using UnityEngine;
using UnityEngine.AI;

public class MageSkelete : MonoBehaviour
{
    public Transform player;
    public float detectRange = 18f;
    public float skill1Range = 12f;
    public float skill2Range = 12f;

    public float skill1Cooldown = 2f;
    public float skill2Cooldown = 4f;
    private float skill1Timer = 0f;
    private float skill2Timer = 0f;

    public GameObject normalBulletPrefab;
    public GameObject fireBulletPrefab;
    public Transform firePoint;

    private Animator animator;
    private NavMeshAgent navAgent;
    private bool isCastingSkill = false;
    private int skill1CastCount = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null || isCastingSkill) return;

        skill1Timer -= Time.deltaTime;
        skill2Timer -= Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectRange && distance > skill1Range)
        {
            navAgent.SetDestination(player.position);
            RotateTowards(player.position);
            animator.SetFloat("Speed", navAgent.velocity.magnitude);
            animator.SetBool("IsAttack", false);
        }
        else if (distance <= skill1Range)
        {
            navAgent.ResetPath();
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsAttack", true);
            RotateTowards(player.position);

            if (skill1CastCount < 2 && skill1Timer <= 0f)
            {
                animator.SetTrigger("Skill1");
                isCastingSkill = true;
                skill1Timer = skill1Cooldown;
                skill1CastCount++;
            }
            else if (skill1CastCount >= 2 && skill2Timer <= 0f && distance <= skill2Range)
            {
                animator.SetTrigger("Skill2");
                isCastingSkill = true;
                skill2Timer = skill2Cooldown;
                skill1CastCount = 0; 
            }
        }
        else
        {
            navAgent.ResetPath();
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsAttack", false);
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction.magnitude > 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
        }
    }

    public void UseSkill1()
    {
        if (player == null) return;

        GameObject bullet = Instantiate(normalBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<MageBulllet>().Initialize(30f, player.position);
    }

    public void UseSkill2()
    {
        if (player == null) return;

        GameObject bullet = Instantiate(fireBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<MageFireBullet>().Initialize(30f, 5f, 3f, player.position);
    }

    public void EndCasting()
    {
        isCastingSkill = false;
        animator.SetBool("IsAttack", false);
    }
}
