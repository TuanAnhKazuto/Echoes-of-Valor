using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Base Stats")]
    public string weaponID;
    public string weaponName;

    public int weaponLevel = 1;
    public int maxWeaponLevel = 60;
    public int weaponBreakthrough = 1;
    public bool isMaxLevel = false;


    [Header("Damage")]
    public int baseDamage;
    public int damagePerLevel = 3;
    public int damagePerBreakthrough = 6;
    public int GetDamage()
    {
        int damage = baseDamage + (weaponLevel - 1) * damagePerLevel;
        return damage;
    }


    [Header("Defense")]
    public int baseDefense;
    public int defensePerLevel = 3;
    public int defensePerBreakthrough = 6;
    public int GetDefense()
    {
        int defense = baseDefense + (weaponLevel - 1) * defensePerLevel;
        return defense;
    }

    [Header("Weapon Models")]
    public GameObject baseWeapon;
    public GameObject lowWeapon;
    public GameObject highWeapon;
    [HideInInspector] public GameObject currentObj;

    [Header("Weapon Upgrade System")]
    public InventoryManager inventoryManager;
    public int ingredientNeedToUpgrade = 0;
    public int ingredientCount = 0;

    public Ingredient currentIngredientUsed;
    public Ingredient upgradeIngredient1;
    public Ingredient upgradeIngredient2;
    public Ingredient upgradeIngredient3;

    public string messageNotEnoughIngredients = "Not enough ingredients to upgrade the weapon.";
    public int GetUpdateIngredientAmount(int currentlevel)
    {
        int levelUp = (currentlevel + 1) * 2;

        return levelUp;
    }

    private void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        currentIngredientUsed = upgradeIngredient1;
    }

    public void LevelUpdate(int levelsToAdd)
    {
        ingredientNeedToUpgrade = GetUpdateIngredientAmount(weaponLevel);

        if (inventoryManager.upgradeIngredients.Find(i => i.ingredient == currentIngredientUsed) != null)
            ingredientCount = inventoryManager.upgradeIngredients.Find(i => i.ingredient == currentIngredientUsed).quantity;
        else
            ingredientCount = 0;

        if (ingredientCount >= ingredientNeedToUpgrade)
        {
            inventoryManager.upgradeIngredients.Find(i => i.ingredient == currentIngredientUsed).quantity -= ingredientNeedToUpgrade;
            LevelUp(levelsToAdd);
        }
        else
        {
            Debug.Log("Not enough ingredients to upgrade the weapon.");
            return;
        }

    }

    private void LevelUp(int levelsToAdd)
    {
        for (int i = 0; i < levelsToAdd; i++)
        {
            weaponLevel++;

            baseDamage += damagePerLevel;
            baseDefense += defensePerLevel;

            if (weaponLevel >= 20 && weaponLevel <= 39)
            {
                weaponBreakthrough = 2;
                currentIngredientUsed = upgradeIngredient2;
            }
            else if (weaponLevel >= 40)
            {
                weaponBreakthrough = 3;
                currentIngredientUsed = upgradeIngredient3;
            }
            else
            {
                weaponBreakthrough = 1;
                currentIngredientUsed = upgradeIngredient1;
                isMaxLevel = false;
            }

            if (weaponLevel >= maxWeaponLevel)
            {
                weaponLevel = maxWeaponLevel;
                isMaxLevel = true;
            }

            RankUpdateControl();
        }
    }

    public void RankUpdateControl()
    {
        switch (weaponBreakthrough)
        {
            case 1:
                baseWeapon.SetActive(true);
                lowWeapon.SetActive(false);
                highWeapon.SetActive(false);
                currentObj = baseWeapon;
                break;
            case 2:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(true);
                highWeapon.SetActive(false);
                currentObj = lowWeapon;
                break;
            case 3:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(false);
                highWeapon.SetActive(true);
                currentObj = highWeapon;
                break;
        }
    }


}