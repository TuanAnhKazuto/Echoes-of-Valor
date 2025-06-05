using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;

    public void UpdateHealth(int curHealth, int maxHealth)
    {
        fillBar.fillAmount = (float)curHealth / (float)maxHealth;
    }
}
