using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Base Stats")]
    public string weaponID;
    public string weaponName;

    public int weaponLevel = 1;
    public int maxWeaponLevel = 60;
    public int weaponBreakthrough = 1;


    [Header("Damage")]
    public int baseDamage;
    public int damagePerLevel = 3;
    public int damagePerBreakthrough = 6;

    [Header("Defense")]
    public int baseDefense;
    public int defensePerLevel = 3;
    public int defensePerBreakthrough = 6;


    public float weaponDamage;
    public float damageBonusWhenUpdate = 5;

    [Header("Weapon Models")]
    public GameObject baseWeapon;
    public GameObject lowWeapon;
    public GameObject highWeapon;

    public GameObject currentObj;

    public int GetDamage()
    {
        int damage = baseDamage + (weaponLevel - 1) * damagePerLevel + (weaponBreakthrough - 1) * damagePerBreakthrough;
        return damage;
    }

    public int GetDefense()
    {
        int defense = baseDefense + (weaponLevel - 1) * defensePerLevel + (weaponBreakthrough - 1) * defensePerBreakthrough;
        return defense;
    }


    public void LevelUpdate(int levelsToAdd)
    {
        for (int i = 0; i < levelsToAdd; i++)
        {
            weaponLevel++;

            weaponDamage += damageBonusWhenUpdate;

            //// Nếu muốn: Breakthrough mỗi 20 cấp
            //if (weaponLevel % 19 == 0)
            //{
            //    weaponBreakthrough++;
            //    RankUpdateControl(); // đổi hình vũ khí
            //}

            if (weaponLevel >= 20 && weaponLevel <= 39)
            {
                weaponBreakthrough = 2; // Low sword
            }
            else if (weaponLevel >= 40)
            {
                weaponBreakthrough = 3; // High sword
            }
            else
            {
                weaponBreakthrough = 1; // Base sword
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
                //weaponDamage += 10f; // Base damage
                break;
            case 2:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(true);
                highWeapon.SetActive(false);
                currentObj = lowWeapon;
                //weaponDamage += 10f; // Low sword damage
                break;
            case 3:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(false);
                highWeapon.SetActive(true);
                currentObj = highWeapon;
                //weaponDamage += 10f; // High sword damage
                break;
        }
    }
}