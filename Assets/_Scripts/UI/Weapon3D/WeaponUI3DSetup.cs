using UnityEngine;
using UnityEngine.UI;

public class WeaponUI3DSetup : MonoBehaviour
{
    public SaveGameManager saveGameManager;

    [Header("Panel Setup")]
    public Image wpBtn01;
    public Image wpBtn02;

    [Header("Weapon sprite in resources")]
    public Sprite imageSword;
    public Sprite imageShield;
    public Sprite imageBow;
    public Sprite imageKnife;
    public Sprite imageStaff;
    public Sprite imageMageBook;

    [Header("UI3DCam Setup")]
    public ComponentInUI3DCam UI3DCam;

    [Header("Switch Weapon Setup")]
    public TabsSwitchManger tabsSwitchMg;
    public UpdateWpUI[] updateWpUI;


    private void Awake()
    {
        tabsSwitchMg = GetComponentInChildren<TabsSwitchManger>();

        UI3DCam = FindAnyObjectByType<ComponentInUI3DCam>();
    }

    private void Start()
    {
        if (saveGameManager.playerStats.characterClass == "Knight")
        {
            wpBtn01.sprite = imageSword;
            wpBtn02.sprite = imageShield;

            UI3DCam.KnightWeapon.SetActive(true);

            for (int i = 0; i <= 1; i++)
            {
                updateWpUI[i].weaponStats = saveGameManager.equippedWeapons[i].GetComponent<WeaponStats>();
                if(i == 0)
                {
                    updateWpUI[i].linkIndexWp2UI = UI3DCam.SwordUIObj.GetComponent<LinkIndexWp2UI>();
                    tabsSwitchMg.weaponObj[i] = UI3DCam.SwordUIObj;
                }
                if (i == 1)
                {
                    updateWpUI[i].linkIndexWp2UI = UI3DCam.ShieldUIObj.GetComponent<LinkIndexWp2UI>();
                    tabsSwitchMg.weaponObj[i] = UI3DCam.ShieldUIObj;
                }
            }
        }
        else if (saveGameManager.playerStats.characterClass == "Rogue")
        {
            wpBtn01.sprite = imageBow;
            wpBtn02.gameObject.SetActive(false);

            for (int i = 0; i >= 2; i++)
            {
                updateWpUI[i].weaponStats = saveGameManager.equippedWeapons[i].GetComponent<WeaponStats>();
            }

            UI3DCam.RogueWeapon.SetActive(true);
            tabsSwitchMg.weaponObj[0] = UI3DCam.BowUIObj;
            tabsSwitchMg.weaponObj[1] = UI3DCam.KnifeUIObj;
        }
        else if (saveGameManager.playerStats.characterClass == "Mage")
        {
            wpBtn01.sprite = imageStaff;
            wpBtn02.sprite = imageMageBook;

            for (int i = 0; i >= 2; i++)
            {
                updateWpUI[i].weaponStats = saveGameManager.equippedWeapons[i].GetComponent<WeaponStats>();
            }

            UI3DCam.MageWeapon.SetActive(true);
            tabsSwitchMg.weaponObj[0] = UI3DCam.StaffUIObj;
            tabsSwitchMg.weaponObj[1] = UI3DCam.MageBookUIObj;
        }
    }
}
