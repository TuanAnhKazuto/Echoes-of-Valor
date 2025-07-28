using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    //public GameObject greenFill;
    //public GameObject yellowFill;
    //public GameObject redFill;

    //public Image greenFillImg;
    //public Image yellowFillImg;
    //public Image redFillImg;
    public Image fillBar;
    public Image fillBarDamageSub;
    public TextMeshProUGUI hpText;

    public TextMeshProUGUI nameText;

    private void Start()
    {
        fillBarDamageSub.fillAmount = fillBar.fillAmount;
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        //greenFillImg.fillAmount = (float)currentHealth / maxHealth;
        //yellowFillImg.fillAmount = (float)currentHealth / maxHealth;
        //redFillImg.fillAmount = (float)currentHealth / maxHealth;

        //if(greenFillImg.fillAmount >= 0.5f)
        //{
        //    greenFill.SetActive(true);
        //    yellowFill.SetActive(false);
        //    redFill.SetActive(false);
        //}
        //else if(greenFillImg.fillAmount >= 0.25f)
        //{
        //    greenFill.SetActive(false);
        //    yellowFill.SetActive(true);
        //    redFill.SetActive(false);
        //}
        //else
        //{
        //    greenFill.SetActive(false);
        //    yellowFill.SetActive(false);
        //    redFill.SetActive(true);
        //}

        fillBar.fillAmount = (float)currentHealth / maxHealth;
        hpText.text = $"{currentHealth}/{maxHealth}";
        StartCoroutine(subHpDelay(currentHealth, maxHealth));
    }

    IEnumerator subHpDelay(int cur, int max)
    {
        //yield return new WaitForSeconds(0.3f);
        //fillBarDamageSub.fillAmount = (float)cur / max;

        while (fillBarDamageSub.fillAmount > fillBar.fillAmount)
        {
            fillBarDamageSub.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
