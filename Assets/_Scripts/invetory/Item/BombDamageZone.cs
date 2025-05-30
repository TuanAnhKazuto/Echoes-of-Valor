using UnityEngine;

public class BombDamageZone : MonoBehaviour
{
    public enum DamageLevel { High, Medium, Low }
    public DamageLevel damageLevel;

    public int highDamage = 100;
    public int mediumDamage = 50;
    public int lowDamage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            int damage = 0;
            switch (damageLevel)
            {
                case DamageLevel.High:
                    damage = highDamage;
                    break;
                case DamageLevel.Medium:
                    damage = mediumDamage;
                    break;
                case DamageLevel.Low:
                    damage = lowDamage;
                    break;
            }
            other.GetComponent<Enemy>()?.TakeDamage(damage);
        }
    }
}
