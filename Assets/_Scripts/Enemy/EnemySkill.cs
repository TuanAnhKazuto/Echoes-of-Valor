using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject lightningVFX;     // VFX sấm sét
    public Transform pointPos;
    public float lightningDuration = 6f;

    private int attackCount = 0;
    private bool isLightningActive = false;

    void PerformAttack()
    {
        attackCount++;

        if (attackCount == 4 && !isLightningActive)
        {
            ActivateLightning();
        }

        // Nếu quái tấn công xong reset combo
        if (attackCount >= 4)
        {
            attackCount = 0;
        }
    }

    void ActivateLightning()
    {
        if (lightningVFX != null)
        {
            GameObject linght = Instantiate(lightningVFX, pointPos.position, Quaternion.identity);
            isLightningActive = true;
            Destroy(linght, lightningDuration);
            Invoke(nameof(DeactivateLightning), lightningDuration);
        }
    }

    void DeactivateLightning()
    {
        if (lightningVFX != null)
        {
            isLightningActive = false;
        }
    }
}
