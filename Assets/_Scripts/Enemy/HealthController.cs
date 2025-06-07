using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth { get; private set; }

    public bool IsDead { get; private set; }

    [Header("UI")]
    public Image healthBarFill;

    void Start()
    {
        if (healthBarFill == null) healthBarFill = this.transform.Find("Health/BG/HealthBar").GetComponent<Image>();
        ResetHealth();
        UpdateHealthBar();
    }

    public void TakeDamage(float amount, BaseCharacter character)
    {
        if (IsDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0f && !IsDead)
        {
            IsDead = true;
            if (healthBarFill != null) healthBarFill.transform.parent.gameObject.SetActive(false);
            character?.Death();
        }
    }

    public void Heal(float amount)
    {
        if (IsDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        IsDead = false;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }
}
