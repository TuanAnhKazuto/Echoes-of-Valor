using UnityEngine;

public class FireBreath : MonoBehaviour
{
    public float damagePerSecond = 9f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
