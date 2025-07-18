using UnityEngine;

public class MageFireBullet : MonoBehaviour
{
    public float speed = 20f;
    private float impactDamage;
    private float burnDamagePerSecond;
    private float burnDuration;
    private Vector3 target;
    private float damage;

    public void Initialize(float dmg, float burnDmg, float duration, Vector3 targetPosition)
    {
        impactDamage = dmg;
        burnDamagePerSecond = burnDmg;
        burnDuration = duration;
        target = targetPosition + Vector3.up * 1.5f; 
        Destroy(gameObject, 1f);
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        {
            CharacterStats ps = other.GetComponent<CharacterStats>();
            if (ps != null)
            {
                ps.TakeDamage(35);
            }
            Destroy(gameObject);
        }
    }
}
