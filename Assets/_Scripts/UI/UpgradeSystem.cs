using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public int e = 60; 

    [Header("Link to Character")]
    public CharacterStats characterStats;

    [Header("Upgrade Count")]
    private int hpUpgradeCount = 0;
    private int manaUpgradeCount = 0;
    private int attackUpgradeCount = 0;
    private int speedUpgradeCount = 0;

    [Header("Upgrade Count Texts")]
    public TMP_Text hpCountText;
    public TMP_Text manaCountText;
    public TMP_Text attackCountText;
    public TMP_Text speedCountText;

    [Header("UI Texts")]
    public TMP_Text eText;
    public TMP_Text hpText;
    public TMP_Text manaText;
    public TMP_Text attackText;
    public TMP_Text speedText;

    [Header("Upgrade Buttons")]
    public Button addHPButton;
    public Button addManaButton;
    public Button addAttackButton;
    public Button addSpeedButton;

    private const int cost = 2;

    [Header("Upgrade Panel")]
    public GameObject upgradePanel;

    private bool isPanelActive = false;

    private void Start()
    {
        if (characterStats == null)
        {
            characterStats = FindAnyObjectByType<CharacterStats>();
        }
        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        addHPButton.onClick.AddListener(AddHP);
        addManaButton.onClick.AddListener(AddMana);
        addAttackButton.onClick.AddListener(AddAttack);
        addSpeedButton.onClick.AddListener(AddSpeed);

        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TogglePanel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPanelActive)
                TogglePanel();
        }
    }

    private void TogglePanel()
    {
        isPanelActive = !isPanelActive;

        if (upgradePanel != null)
            upgradePanel.SetActive(isPanelActive);

        Time.timeScale = isPanelActive ? 0 : 1;
    }

    private void AddHP()
    {
        if (e >= cost && characterStats != null)
        {
            e -= cost;
            characterStats.maxHealth += 25;
            characterStats.currentHealth = characterStats.maxHealth; // hồi đầy máu khi nâng cấp
            characterStats.healthBar.UpdateHealth((int)characterStats.currentHealth, (int)characterStats.maxHealth);

            hpUpgradeCount++;
            UpdateUI();
        }
    }

    private void AddMana()
    {
        if (e >= cost && characterStats != null)
        {
            e -= cost;
            characterStats.maxMana += 10;
            characterStats.currentMana = characterStats.maxMana; // hồi đầy mana khi nâng cấp
            characterStats.manaBar.UpdateMana(characterStats.currentMana, characterStats.maxMana);

            manaUpgradeCount++;
            UpdateUI();
        }
    }

    private void AddAttack()
    {
        if (e >= cost && characterStats != null)
        {
            e -= cost;
            characterStats.baseDamage += 1;

            attackUpgradeCount++;
            UpdateUI();
        }
    }

    private void AddSpeed()
    {
        if (e >= cost)
        {
            e -= cost;
            characterStats.baseDefense += 1; // tạm thời thay cho speed nếu chưa có biến speed

            speedUpgradeCount++;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (characterStats == null) return;

        eText.text = "Echo: " + e;
        hpText.text = "Máu tối đa: " + characterStats.maxHealth;
        manaText.text = "Mana tối đa: " + characterStats.maxMana;
        attackText.text = "Tấn công: " + characterStats.baseDamage;
        speedText.text = "Phòng thủ: " + characterStats.baseDefense;

        hpCountText.text = "" + hpUpgradeCount;
        manaCountText.text = "" + manaUpgradeCount;
        attackCountText.text = "" + attackUpgradeCount;
        speedCountText.text = "" + speedUpgradeCount;
    }
}
