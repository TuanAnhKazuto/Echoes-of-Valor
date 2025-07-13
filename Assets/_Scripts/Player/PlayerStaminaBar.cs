using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    public Image staminaFill;
    public GameObject staminaObj;

    bool isStaminaBarActive = false;

    private void Update()
    {
        if(staminaFill.fillAmount >= 1)
        {
            if (!isStaminaBarActive) return;
            Invoke(nameof(HideStaminaBar), .2f);
        }
        else
        {
            staminaObj.SetActive(true);
            isStaminaBarActive = true;
        }
    }

    private void HideStaminaBar()
    {
        staminaObj.SetActive(false);
        isStaminaBarActive = false;
    }

    public void UpdateBar(int currentStamina, int maxStamina)
    {
        if (maxStamina <= 0)
        {
            staminaFill.fillAmount = 0f;
            return;
        }
        staminaFill.fillAmount = (float)currentStamina / maxStamina;
    }
}
