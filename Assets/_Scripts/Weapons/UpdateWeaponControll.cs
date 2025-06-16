using UnityEngine;

public class UpdateWeaponControll : MonoBehaviour
{
    public GameObject baseWeapon;
    public GameObject lowWeapon;
    public GameObject highWeapon;

    public int weaponLevel = 1;
    public int weaponBreakthrough = 1;

    public float weaponDamage;
    public float damageBonusWhenUpdate = 5;

    private void Update()
    {
        RankUpdateControl();
    }

    public void LevelUpdate(int levelsToAdd)
    {
        for (int i = 0; i < levelsToAdd; i++)
        {
            weaponLevel++;

            weaponDamage += damageBonusWhenUpdate;

            // Nếu muốn: Breakthrough mỗi 20 cấp
            if (weaponLevel % 20 == 0)
            {
                weaponBreakthrough++;
                Debug.Log($"Vũ khí đã đột phá cấp {weaponBreakthrough}!");
                RankUpdateControl(); // đổi hình vũ khí
            }
        }

        Debug.Log($"Đã tăng {levelsToAdd} cấp. Damage hiện tại: {weaponDamage}");
    }

    public void RankUpdateControl()
    {
        switch (weaponBreakthrough)
        {
            case 1:
                baseWeapon.SetActive(true);
                lowWeapon.SetActive(false);
                highWeapon.SetActive(false);
                //weaponDamage += 10f; // Base damage
                break;
            case 2:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(true);
                highWeapon.SetActive(false);
                //weaponDamage += 10f; // Low sword damage
                break;
            case 3:
                baseWeapon.SetActive(false);
                lowWeapon.SetActive(false);
                highWeapon.SetActive(true);
                //weaponDamage += 10f; // High sword damage
                break;
        }
    }
}
