using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillBar;

    public void UpdateHealth(int curHealth, int maxHealth)
    {
        fillBar.fillAmount = (float)curHealth / (float)maxHealth;
    }
}
