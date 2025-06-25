using UnityEngine;

public class MageBullet : MonoBehaviour
{
    public GameObject fx;
    
    public int damge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterStats characterStats))
        {
            Instantiate(fx, transform.position, transform.rotation);
            Debug.Log("Character TakeDamage");
            
            Destroy(gameObject);
        }

        if (other.gameObject.name.Contains("Cube"))
        {
            Destroy(gameObject);
        }
    }
}
