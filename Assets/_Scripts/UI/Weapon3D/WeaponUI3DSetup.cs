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
    public TabsController tabsController;
    public UpdateWpUI[] updateWpUI;


    private void Awake()
    {
        tabsController = GetComponentInChildren<TabsController>();

        UI3DCam = FindAnyObjectByType<ComponentInUI3DCam>();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                    tabsController.weaponObj[i] = UI3DCam.SwordUIObj;
                }
                if (i == 1)
                {
                    updateWpUI[i].linkIndexWp2UI = UI3DCam.ShieldUIObj.GetComponent<LinkIndexWp2UI>();
                    tabsController.weaponObj[i] = UI3DCam.ShieldUIObj;
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
            tabsController.weaponObj[0] = UI3DCam.BowUIObj;
            tabsController.weaponObj[1] = UI3DCam.KnifeUIObj;
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
            tabsController.weaponObj[0] = UI3DCam.StaffUIObj;
            tabsController.weaponObj[1] = UI3DCam.MageBookUIObj;
        }
    }
}
