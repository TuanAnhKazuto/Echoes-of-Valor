using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    private float damage;

    public void Initialize(float damageAmount)
    {
        damage = damageAmount;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
