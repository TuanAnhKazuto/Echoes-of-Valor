using System.Collections;
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
    public Image ingerdientImg;
    public TextMeshProUGUI ingerdientCountText;

    public GameObject updateSystemObj;
    public GameObject messengerObj;
    public TextMeshProUGUI messengerText;

    int levelsToAdd = 1;

    private void Start()
    {
        StartCoroutine(DelayRefreshDisplay());
        if (weaponStats != null)
            upgradeValuerSlider.maxValue = 20;

        ingerdientCountText.text = "0 / 4";
    }

    private IEnumerator DelayRefreshDisplay()
    {
        yield return new WaitForSecondsRealtime(0.2f); // không bị ảnh hưởng bởi timeScale
        RefreshDisplay();
    }


    private void Update()
    {
        linkIndexWp2UI.ChangeWeaponUI(weaponStats.weaponBreakthrough);
    }

    public void UpdaterValuerUpgrade()
    {
        levelUpdateText.text = upgradeValuerSlider.value.ToString();
        levelsToAdd = (int)upgradeValuerSlider.value;
        ingerdientCountText.text = weaponStats.ingredientCount.ToString() + " / " + weaponStats.ingredientNeedToUpgrade.ToString();

    }

    public void UpdateBtn()
    {
        levelsToAdd = (int)upgradeValuerSlider.value;

        weaponStats.LevelUpdate(levelsToAdd);

        upgradeValuerSlider.maxValue = 20 - weaponStats.weaponLevel;
        
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

        ingerdientImg.sprite = weaponStats.currentIngredientUsed.icon;
        ingerdientCountText.text = weaponStats.ingredientCount.ToString() + " / " + weaponStats.ingredientNeedToUpgrade.ToString();

        upgradeValuerSlider.value = 1;
    }
}