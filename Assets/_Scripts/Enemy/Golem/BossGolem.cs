using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BossGolem : MonoBehaviour
{
    public enum BossState
    {
        Idle,
        Chase,
        Attack,
        UseSkill,
        Enraged,
        Dead
    }

    public enum BossSkillType
    {
        Skill1_JumpAttack,
        Skill2,
        Skill3
    }

    private BossSkillType selectedSkill;
    private bool isUsingSkill = false;



    public BossState currentState;
    public Transform player;

    private Animator anim;
    private NavMeshAgent agent;
    public EnemyHealthBar healthBar;
    private float distanceToPlayer;
    public Next endScene;

    [Header("Boss Settings")]
    public float detectionRange = 20f;
    public float attackRange = 5f;
    public float skillCooldown = 10f;
    [SerializeField] private float skillTimer;

    [Header("Boss Stats")]
    public float maxHealth = 100f;  
    public float currentHealth; 
    public float baseDamage = 30f;
    public float level = 50f;
    public TextMeshProUGUI levelText;

    [Header("Effect")]
    public GameObject groundBreakEffectPrefab;
    public Transform groundBreakEffectSpawnPoint;

    public void HamMa()
    {

    }


    void Start()
    {
        endScene = GetComponent<Next>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = BossState.Idle;

        currentHealth = maxHealth;
        healthBar.UpdateHealth((int)currentHealth, (int)maxHealth);
        anim.SetFloat("HP", currentHealth);
        levelText.text = "Lv: " + level.ToString();
    }

    void Update()
    {
        if (currentState != BossState.Dead)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            skillTimer += Time.deltaTime;

            StateMachine();
        }
    }

    void StateMachine()
    {
        switch (currentState)
        {
            case BossState.Idle:
                if (distanceToPlayer <= detectionRange)
                    ChangeState(BossState.Chase);
                break;

            case BossState.Chase:
                ChasePlayer();
                break;

            case BossState.Attack:
                AttackPlayer();
                break;

            case BossState.UseSkill:
                UseSkill();
                break;

            case BossState.Enraged:

                break;

            case BossState.Dead:

                break;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetFloat("Speed", agent.velocity.magnitude);
        anim.SetBool("IsAttack", false);

        if (distanceToPlayer <= attackRange)
        {
            ChangeState(BossState.Attack);
        }
        else if (skillTimer >= skillCooldown)
        {
            ChangeState(BossState.UseSkill);
        }
    }

    void AttackPlayer()
    {
        baseDamage = 120f;
        agent.SetDestination(transform.position);
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        anim.SetBool("IsAttack", true);
        anim.SetFloat("Speed", 0f);

        if (distanceToPlayer > attackRange)
            ChangeState(BossState.Chase);
    }

    void UseSkill()
    {
        agent.SetDestination(transform.position);
        anim.SetBool("IsAttack", false);
        isUsingSkill = true;

        selectedSkill = (BossSkillType)Random.Range(0, 2);

        switch (selectedSkill)
        {
            case BossSkillType.Skill1_JumpAttack:
                Skill1_JumpAttack();
                //StartCoroutine(Skill1_JumpAttack());
                break;
                // case BossSkillType.Skill2:
                //     StartCoroutine(Skill2());
                //     break;
        }

    }

    public void Skill1_JumpAttack()
    {
        baseDamage = 200;
        anim.SetTrigger("UseSkill01");

        // Ngưng di chuyển
        agent.enabled = false;

        agent.enabled = true;

        isUsingSkill = false;
        skillTimer = 0f;
        ChangeState(BossState.Chase);

    }

    //IEnumerator Skill1_JumpAttack()
    //{
    //    baseDamage = 120f;
    //    anim.SetTrigger("UseSkill01");

    //    // Ngưng di chuyển
    //    agent.enabled = false;

    //    Rigidbody rb = GetComponent<Rigidbody>();
    //    rb.linearVelocity = Vector3.zero;

    //    // Nhảy lên bằng lực
    //    float jumpForce = 7f; // chỉnh cho phù hợp
    //    rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

    //    // Chờ cho đến khi boss rơi xuống đất
    //    yield return new WaitUntil(() => Mathf.Abs(rb.linearVelocity.y) < 0.01f && IsGrounded());

    //    //anim.SetTrigger("Land"); // nếu có animation tiếp đất

    //    yield return new WaitForSeconds(0.5f);

    //    // Kích hoạt lại NavMeshAgent
    //    agent.enabled = true;

    //    isUsingSkill = false;
    //    skillTimer = 0f;
    //    ChangeState(BossState.Chase);
    //}

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f, LayerMask.GetMask("Ground"));
    }

    public void SpawnGroundBreakEft()
    {
        GameObject eft = Instantiate(groundBreakEffectPrefab, groundBreakEffectSpawnPoint.position, groundBreakEffectSpawnPoint.rotation);
        Destroy(eft, 4f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealth((int)currentHealth, (int)maxHealth);

        anim.SetFloat("HP", currentHealth);
        anim.SetLayerWeight(1, 0.7f);
        anim.SetBool("Hit", false);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Invoke(nameof(Die), 0.5f);
            ChangeState(BossState.Dead);
        }

    }

    public void OffTakeDamageAnim()
    {
        anim.SetLayerWeight(1, 0f);
        anim.SetBool("Hit", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHitBox"))
        {
            CharacterStats characterStats = other.GetComponentInParent<CharacterStats>();
            if (characterStats != null)
            {
                TakeDamage(characterStats.TotalDamage);
            }
            anim.SetLayerWeight(1, 0.7f);
            anim.SetBool("Hit", true);
        }
    }

    void ChangeState(BossState newState)
    {
        currentState = newState;
    }

    public void Die()
    {
        currentState = BossState.Dead;
        anim.SetFloat("HP", currentHealth);
        endScene.NextBtn();
        Invoke(nameof(DelayDestroy), 1.5f);
        agent.enabled = false;
    }

    private void DelayDestroy()
    {
        Destroy(gameObject);
    }
}