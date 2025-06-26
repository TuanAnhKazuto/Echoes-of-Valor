using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public PlayerHealthBar healthBar;
    public PlayerManaBar manaBar;
    public Animator animator;
    public GameObject CursorTarget;

    [Header("Base Stats")]
    public int playerId;
    public string playerName;
    public string characterClass;

    public int level = 1;
    public float maxHealth = 100f;
    public float currentHealth;
    public float currentMana;
    public float maxMana = 100f;

    public int baseDamage = 10;
    public int baseDefense = 5;

    public bool isDied = false;

    [Header("Equipment")]
    public WeaponStats[] equippedWeapons;

    private void Awake()
    {
        if (healthBar == null)
        {
            healthBar = FindAnyObjectByType<PlayerHealthBar>();
        }
        if (manaBar == null)
        {
            manaBar = FindAnyObjectByType<PlayerManaBar>();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        animator.SetFloat("HP", currentHealth);
        CursorTarget.SetActive(true);
    }

    public int TotalDamage => baseDamage + equippedWeapons[0].baseDamage;

    public int TotalDefense => baseDamage + (equippedWeapons[1]?.baseDefense ?? 0) + (equippedWeapons[1]?.baseDefense ?? 0);

    public void TakeDamage(float damage)
    {
        float damageTake = Mathf.Max(damage - TotalDefense, 1f);
        currentHealth -= damageTake;
        healthBar.UpdateHealth((int)currentHealth, (int)maxHealth);
        animator.SetTrigger("TakeDamage");
        animator.SetFloat("HP", currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            CursorTarget.SetActive(false);
            animator.SetFloat("HP", currentHealth);
            isDied = true;
            Invoke(nameof(Die), 1);
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.UpdateHealth((int)currentHealth, (int)maxHealth);
        animator.SetFloat("HP", currentHealth);
    }
   
    public bool ConsumeMana(float amount)
    {
        if (amount > 0)
        {
            
            if (currentMana >= amount)
            {
                currentMana -= amount;
                manaBar.UpdateMana(currentMana, maxMana);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            currentMana = Mathf.Min(currentMana - amount, maxMana);
            manaBar.UpdateMana(currentMana, maxMana);
            return true;
        }
    }

    private void Die()
    {
        GameResult gameResult = FindAnyObjectByType<GameResult>();
        if (gameResult != null)
            gameResult.ShowFailPanel();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SkeletonArrow"))
        {
            SkeletonArrow arrow = other.GetComponent<SkeletonArrow>();
            TakeDamage(arrow.damage);
        }

        if(other.gameObject.transform.CompareTag("EnemyHitBox"))
        {
            TakeDamage(20f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            Heal(10f); 
        }
    }
}