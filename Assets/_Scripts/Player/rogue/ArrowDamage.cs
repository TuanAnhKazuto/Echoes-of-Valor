using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    public float damage = 20f;
    public ParticleSystem onAOEImpactEffect;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log($"Dealt {damage} damage to {collision.gameObject.name}");

            if (onAOEImpactEffect != null)
            {
                Instantiate(onAOEImpactEffect, transform.position, Quaternion.identity).Play();
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
