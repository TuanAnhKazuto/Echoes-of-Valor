using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public GameObject greenFill;
    public GameObject yellowFill;
    public GameObject redFill;

    public Image greenFillImg;
    public Image yellowFillImg;
    public Image redFillImg;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        greenFillImg.fillAmount = (float)currentHealth / maxHealth;
        yellowFillImg.fillAmount = (float)currentHealth / maxHealth;
        redFillImg.fillAmount = (float)currentHealth / maxHealth;

        if(greenFillImg.fillAmount >= 0.5f)
        {
            greenFill.SetActive(true);
            yellowFill.SetActive(false);
            redFill.SetActive(false);
        }
        else if(greenFillImg.fillAmount >= 0.25f)
        {
            greenFill.SetActive(false);
            yellowFill.SetActive(true);
            redFill.SetActive(false);
        }
        else
        {
            greenFill.SetActive(false);
            yellowFill.SetActive(false);
            redFill.SetActive(true);
        }
    }
}
