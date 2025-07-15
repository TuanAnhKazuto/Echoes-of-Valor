using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWpUI : MonoBehaviour
{
    public WeaponStats weaponStats;
    public LinkIndexWp2UI linkIndexWp2UI;

    [Header("UI Elements")]
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponLevelText;
    public TextMeshProUGUI weaponBreakthroughText;
    public TextMeshProUGUI weaponDamageText;
    public TextMeshProUGUI weaponDefenseText;

    public Slider upgradeValuerSlider;
    public TextMeshProUGUI levelUpdateText;

    public GameObject updateSystemObj;
    public GameObject messengerObj;

    int levelsToAdd = 1;

    private void Start()
    {
        Invoke(nameof(RefreshDisplay), 0.2f);
        
        if(weaponStats != null)
        {
            upgradeValuerSlider.maxValue = weaponStats.maxWeaponLevel;
        }
    }

    private void Update()
    {
        linkIndexWp2UI.ChangeWeaponUI(weaponStats.weaponBreakthrough);
    }

    public void UpdaterValuerUpgrade()
    {
        levelUpdateText.text = upgradeValuerSlider.value.ToString();
    }

    public void UpdateBtn()
    {
        levelsToAdd = (int)upgradeValuerSlider.value;

        weaponStats.LevelUpdate(levelsToAdd);

        upgradeValuerSlider.maxValue = weaponStats.maxWeaponLevel - weaponStats.weaponLevel;

        if (weaponStats.isMaxLevel)
        {
            messengerObj.SetActive(true);
            updateSystemObj.SetActive(false);
        }
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        weaponNameText.text = "Name: " + weaponStats.weaponName;
        weaponLevelText.text = "Level: " + weaponStats.weaponLevel.ToString();
        weaponBreakthroughText.text = "Breakthrough: " + weaponStats.weaponBreakthrough.ToString();
        weaponDamageText.text = "Damage: " + weaponStats.baseDamage.ToString();
        weaponDefenseText.text = "Defense: " + weaponStats.baseDefense.ToString();

        upgradeValuerSlider.value = 1;
    }
}