using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string weaponID;
    public string weaponName;

    public int waponLevel = 1;
    public int maxWaponLevel = 60;
    public int breakthroughLevel = 1;

    public int baseDamage;
    public int damagePerLevel = 3;
    public int damagePerBreakthrough = 6;

    public int baseDefense;
    public int defensePerLevel = 3;
    public int defensePerBreakthrough = 6;

    public int GetDamage()
    {
        int damage = baseDamage + (waponLevel - 1) * damagePerLevel + (breakthroughLevel - 1) * damagePerBreakthrough; 
        return damage;
    }

    public int GetDefense()
    {
        int defense = baseDefense + (waponLevel - 1) * defensePerLevel + (breakthroughLevel - 1) * defensePerBreakthrough; 
        return defense;
    }


    public bool TryUpgradeWeapon()
    {
        if(waponLevel >= maxWaponLevel)
        {
            return false;
        }

        waponLevel++;

        if(waponLevel % 20 == 0) 
        {
            breakthroughLevel++;
            Debug.Log("Breakthrough level increased to: " + breakthroughLevel);
        }

        return true;
    }

}
