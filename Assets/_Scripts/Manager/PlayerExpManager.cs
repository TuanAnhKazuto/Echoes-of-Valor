using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpManager : MonoBehaviour
{
    [SerializeField] AnimationCurve expCurve;
    public CharacterStats characterStats;

    public int currentLevel, totalExp;
    int prevLevelExp, nextLevelExp;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] Image expFill;

    private void Start()
    {
        //currentLevel = 1;
        //totalExp = 32;
        UpdateLevel();
        UpdateInterface();
    }

    public void AddExp(int amount)
    {
        if (currentLevel >= 30) return;
        totalExp += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    public void AddLevel(int amount)
    {
        currentLevel += amount;

        if (currentLevel >= 30)
        {
            currentLevel = 30;
            totalExp = 10000;
            expFill.fillAmount = 1f;
            UpdateLevel();
            UpdateInterface();
            StartCoroutine(characterStats.PlayLevelUpEffect());
            return;
        }


        UpdateLevel();
        totalExp = prevLevelExp;
        UpdateInterface();

        UpdateCharacterStats(amount);
        StartCoroutine(characterStats.PlayLevelUpEffect());
    }

    public void CheckForLevelUp()
    {
        if (totalExp >= nextLevelExp)
        {
            currentLevel++;
            UpdateLevel();
            UpdateInterface();

            // Logic for level up
            UpdateCharacterStats(1);
            StartCoroutine(characterStats.PlayLevelUpEffect());
        }
    }

    void UpdateCharacterStats(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (characterStats != null)
            {
                if (currentLevel <= 10)
                {

                    characterStats.maxHealth += 10f;
                    characterStats.currentHealth += 10f;
                    characterStats.maxMana += 20f;
                    characterStats.currentMana += 20f;
                    characterStats.baseDamage += 2;
                }
                else if (currentLevel <= 20)
                {
                    characterStats.maxHealth += 20f;
                    characterStats.currentHealth += 20f;
                    characterStats.maxMana += 30f;
                    characterStats.currentMana += 30f;
                    characterStats.baseDamage += 3;
                }
                else
                {
                    characterStats.maxHealth += 30f;
                    characterStats.currentHealth += 30f;
                    characterStats.maxMana += 40f;
                    characterStats.currentMana += 40f;
                    characterStats.baseDamage += 4;
                }

                characterStats.healthBar.UpdateHealth((int)characterStats.currentHealth, (int)characterStats.maxHealth);
                characterStats.manaBar.UpdateMana((int)characterStats.currentMana, (int)characterStats.maxMana);
            }
        }

    }

    public void UpdateLevel()
    {
        prevLevelExp = (int)expCurve.Evaluate(currentLevel);
        nextLevelExp = (int)expCurve.Evaluate(currentLevel + 1);
        //UpdateInterface();
    }

    public void UpdateInterface()
    {
        int start = totalExp - prevLevelExp;
        int end = nextLevelExp - prevLevelExp;

        levelText.text = "Lv. " + currentLevel;

        if (currentLevel >= 30)
        {
            expText.text = "MAX LEVEL";
            expFill.fillAmount = 1f;
        }
        else
        {
            expText.text = start + " / " + end + " EXP";
        }
        expFill.fillAmount = (float)start / (float)end;
    }
}
