using TMPro;
using UnityEngine;

public class UpdateWpUI : MonoBehaviour
{
    public WeaponStats weaponStats;
    public LinkIndexWp2UI linkIndexWp2UI;

    [Header("UI Elements")]
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponLevelText;
    public TextMeshProUGUI weaponDamageText;
    public TextMeshProUGUI weaponDefenseText;

    public TextMeshProUGUI levelUpdateText;
    int levelsToAdd = 1;

    private void Start()
    {
        Invoke(nameof(RefreshDisplay), 0.2f);
    }

    private void Update()
    {
        linkIndexWp2UI.ChangeWeaponUI(weaponStats.weaponBreakthrough);
    }

    public void UpdateBtn()
    {
        if (weaponStats.weaponLevel >= 60) return;

        levelsToAdd = 1;
        levelUpdateText.text = levelsToAdd.ToString();

        if (int.TryParse(levelUpdateText.text, out int parsed))
        {
            levelsToAdd = Mathf.Clamp(parsed, 1, 60);
        }

        weaponStats.LevelUpdate(levelsToAdd);
        RefreshDisplay();
    }

    public void AddBtn()
    {
        if(levelsToAdd >= 60) return;
        levelsToAdd++;
        levelUpdateText.text = levelsToAdd.ToString();
    }
    public void SubtractBtn()
    {
        if (levelsToAdd <= 1) return;
        levelsToAdd--;
        levelUpdateText.text = levelsToAdd.ToString();
    }

    void RefreshDisplay()
    {
        weaponNameText.text = "Name: " + weaponStats.weaponName;
        weaponLevelText.text = "Level: " + weaponStats.weaponLevel.ToString();
        weaponDamageText.text = "Damage: " + weaponStats.weaponDamage.ToString("F1");
        weaponDefenseText.text = "Defense: " + weaponStats.weaponBreakthrough.ToString("F1");

        levelUpdateText.text = "1";
    }
}