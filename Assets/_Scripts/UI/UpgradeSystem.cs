using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UpgradeSystem : MonoBehaviour
{
    private int e;

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
        e = 20;

        if (characterStats == null)
            characterStats = FindAnyObjectByType<CharacterStats>();

        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        addHPButton?.onClick.AddListener(AddHP);
        addManaButton?.onClick.AddListener(AddMana);
        addAttackButton?.onClick.AddListener(AddAttack);
        addSpeedButton?.onClick.AddListener(AddSpeed);

        StartCoroutine(InitUI());
    }

    private IEnumerator InitUI()
    {
        yield return null;
        if (characterStats == null)
            characterStats = FindAnyObjectByType<CharacterStats>();
        ResetUpgrades();
    }

    public void ResetUpgrades()
    {
        e = 20;

        hpUpgradeCount = 0;
        manaUpgradeCount = 0;
        attackUpgradeCount = 0;
        speedUpgradeCount = 0;

        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            TogglePanel();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPanelActive)
            TogglePanel();
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
            characterStats.currentHealth = characterStats.maxHealth;
            characterStats.healthBar?.UpdateHealth((int)characterStats.currentHealth, (int)characterStats.maxHealth);

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
            characterStats.currentMana = characterStats.maxMana;
            characterStats.manaBar?.UpdateMana(characterStats.currentMana, characterStats.maxMana);

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
        if (e >= cost && characterStats != null)
        {
            e -= cost;
            characterStats.baseDefense += 1;

            speedUpgradeCount++;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (characterStats == null) return;

        if (eText != null) eText.text = "Echo: " + e;
        if (hpText != null) hpText.text = "Máu tối đa: " + characterStats.maxHealth;
        if (manaText != null) manaText.text = "Mana tối đa: " + characterStats.maxMana;
        if (attackText != null) attackText.text = "Tấn công: " + characterStats.baseDamage;
        if (speedText != null) speedText.text = "Phòng thủ: " + characterStats.baseDefense;

        if (hpCountText != null) hpCountText.text = hpUpgradeCount.ToString();
        if (manaCountText != null) manaCountText.text = manaUpgradeCount.ToString();
        if (attackCountText != null) attackCountText.text = attackUpgradeCount.ToString();
        if (speedCountText != null) speedCountText.text = speedUpgradeCount.ToString();
    }
}
