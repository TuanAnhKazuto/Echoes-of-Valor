using UnityEngine;

public class MageBullet : MonoBehaviour
{
    public int damge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterStats characterStats))
        {
            Debug.Log("Character TakeDamage");
            characterStats.TakeDamage(damge);
            Destroy(gameObject);
        }

        if (other.gameObject.name.Contains("Cube"))
        {
            Destroy(gameObject);
        }
    }
}
