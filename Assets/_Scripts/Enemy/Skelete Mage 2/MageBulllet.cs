using UnityEngine;

public class MageBulllet : MonoBehaviour
{
    public float speed = 15f;
    private float damage;
    private Vector3 target;

    public void Initialize(float dmg, Vector3 targetPosition)
    {
        damage = dmg;
        target = targetPosition + Vector3.up * 1.5f;
        Destroy(gameObject, 3f);
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
                ps.TakeDamage(25);
            }

            Destroy(gameObject);
        }
    }
}
