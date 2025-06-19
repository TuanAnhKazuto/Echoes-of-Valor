using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string weaponID;
    public string weaponName;

    public int weaponLevel = 1;
    public int maxWeaponLevel = 60;
    public int breakthroughLevel = 1;

    public int curDamage;
    public int damagePerLevel = 3;
    public int damagePerBreakthrough = 6;

    public int curDefense;
    public int defensePerLevel = 3;
    public int defensePerBreakthrough = 6;
}