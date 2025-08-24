using UnityEngine;
using UnityEngine.UI;

public class WeaponUI3DSetup : MonoBehaviour
{
    public SaveGameManager saveGameManager;

    [Header("Panel Setup")]
    public Image wpBtn01;
    public Image wpBtn02;
    public bool isPanelOpen = false;

    [Header("Weapon sprite in resources")]
    public Sprite imageSword;
    public Sprite imageShield;
    public Sprite imageBow;
    public Sprite imageQuiver;
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
        isPanelOpen = false;
        //Time.timeScale = 1f;
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
            wpBtn02.sprite = imageQuiver;

            UI3DCam.RogueWeapon.SetActive(true);

            for (int i = 0; i <= 1; i++)
            {
                updateWpUI[i].weaponStats = saveGameManager.equippedWeapons[i].GetComponent<WeaponStats>();
                if (i == 0)
                {
                    updateWpUI[i].linkIndexWp2UI = UI3DCam.BowUIObj.GetComponent<LinkIndexWp2UI>();
                    tabsController.weaponObj[i] = UI3DCam.BowUIObj;
                }
                if (i == 1)
                {
                    updateWpUI[i].linkIndexWp2UI = UI3DCam.QuiverUIObj.GetComponent<LinkIndexWp2UI>();
                    tabsController.weaponObj[i] = UI3DCam.QuiverUIObj;
                }
            }
        }
        else if (saveGameManager.playerStats.characterClass == "Mage")
        {
            wpBtn01.sprite = imageStaff;
            wpBtn02.sprite = imageMageBook;

            UI3DCam.MageWeapon.SetActive(true);

            for (int i = 0; i <= 1; i++)
            {
                updateWpUI[i].weaponStats = saveGameManager.equippedWeapons[i].GetComponent<WeaponStats>();
                if (i == 0)
                {
                    updateWpUI[i].linkIndexWp2UI = UI3DCam.StaffUIObj.GetComponent<LinkIndexWp2UI>();
                    tabsController.weaponObj[i] = UI3DCam.StaffUIObj;
                }
                if (i == 1)
                {
                    updateWpUI[i].linkIndexWp2UI = UI3DCam.MageBookUIObj.GetComponent<LinkIndexWp2UI>();
                    tabsController.weaponObj[i] = UI3DCam.MageBookUIObj;
                }
            }
        }
    }
}
