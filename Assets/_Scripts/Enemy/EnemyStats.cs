using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Component")]
    public CharacterStats characterStats;
    [SerializeField] public SaveGameManager saveGameManager;
    [SerializeField] PlayerExpManager playerExpManager;
    public SkeletonMovement skeletonMovement;

    public EnemyHealthBar healthBar;
    public TextMeshProUGUI levelText;

    [Header("Base Stats")]
    public int level = 1;

    public int maxHealth = 10;
    public int currentHealth;

    public float baseDamage = 10f;

    public bool isDie = false;

    [Header("LevelUp")]
    public float healthPerLevel = 20f;
    public float damagePerLevel = 5f;

    [Header("Quest")]
    public string questTag = "Enemy_Main";

    [Header("VFX")]
    public GameObject damagePopupPrefab;

    [Header("DropItem")]
    public GameObject dropItem;
    public IngredientPickup ingredientPickup;

    [Header("Drop Prefabs")]     
    //public GameObject expPrefab;
    public int expDropCount = 20;    
    //public int expValuePerOrb = 1;

    private void Start()
    {
        skeletonMovement = GetComponent<SkeletonMovement>();
        ingredientPickup = GetComponent<IngredientPickup>();
        saveGameManager = FindAnyObjectByType<SaveGameManager>();

        currentHealth = maxHealth;
        skeletonMovement.animator.SetFloat("HP", currentHealth);
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        Invoke(nameof(GetComponnet), 2f);
    }

    void GetComponnet()
    {
        if (characterStats == null)
        {
            characterStats = saveGameManager.playerStats;
            return;
        }

        if (playerExpManager == null)
        {
            playerExpManager = saveGameManager.playerStats.expManager;
            return;
        }
    }

    public void LevelUp()
    {
        level++;
        maxHealth += (int)healthPerLevel;
        baseDamage += damagePerLevel;
        currentHealth = maxHealth; // Reset current health to max health on level up
        levelText.text = "Lv. " + level.ToString();
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        healthBar.UpdateHealth(currentHealth, maxHealth);
        skeletonMovement.animator.SetFloat("HP", currentHealth);

        skeletonMovement.animator.SetLayerWeight(1, 0.7f);
        skeletonMovement.animator.SetBool("Hit", true);

        if (currentHealth > 0)
        {
            DamagePopupSpawner.Instance.ShowDamage(transform.position + Vector3.up * 2f, (int)damage, Color.red);// effect - HP
        }

        if (currentHealth <= 0)
        {
            isDie = true;
            currentHealth = 0;
            healthBar.UpdateHealth(currentHealth, maxHealth);
            healthBar.gameObject.SetActive(false);
            healthBar.cursorTarget.SetActive(false);
            skeletonMovement.animator.SetLayerWeight(1, 0f);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerQuest = player.GetComponent<PlayerQuest>();
                if (playerQuest != null)
                {
                    playerQuest.UpdateQuest(questTag);
                    Debug.Log($"Cập nhật nhiệm vụ với questTag: {questTag}");
                }
            }
           
        }
    }

    public void Death()
    {
        DropItem();

        playerExpManager.AddExp(expDropCount);

        //DropEXP();
        ingredientPickup.Pickup();
        //StartCoroutine(ingredientPickup.Pickup());
        Destroy(gameObject);  
    }

    public void DropItem()
    {
        Vector3 dropPos = new();

        dropPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        GameObject item = Instantiate(dropItem, dropPos, Quaternion.identity);
    }

    //public void DropEXP()
    //{
    //    Vector3 dropEXP = new();

    //    dropEXP = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

    //    for(int i = 0; i < expDropCount; i++)
    //    {
    //        GameObject item = Instantiate(expPrefab, dropEXP, Quaternion.identity);
    //    }
        
    //    Debug.Log("exp");
    //}

    public void OffTakeDamageAnim()
    {
        skeletonMovement.animator.SetLayerWeight(1, 0f); 
        skeletonMovement.animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            if (characterStats == null)
            {
                characterStats = other.GetComponentInParent<CharacterStats>();
                //playerExpManager = characterStats.expManager;
            }

            TakeDamage(characterStats.TotalDamage);
        }

        if (other.gameObject.CompareTag("PlayerSkill"))
        {
            var skillDamage = other.gameObject.GetComponent<SkillInfo>();
            TakeDamage(skillDamage.damgeSkill);
        }
    }

}
