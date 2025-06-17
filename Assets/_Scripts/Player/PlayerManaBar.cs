using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBar : MonoBehaviour
{
    public Image manaFillImage;

    public void UpdateMana(float currentMana, float maxMana)
    {
        if (manaFillImage != null && maxMana > 0)
        {
            manaFillImage.fillAmount = currentMana / maxMana;
        }
    }
}
