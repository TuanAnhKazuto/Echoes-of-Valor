using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject damageZones; 

    void Start()
    {
        Invoke(nameof(Explode), 2f); 
    }

    void Explode()
    {
        if (damageZones != null)
            damageZones.SetActive(true);
        Destroy(gameObject, 1f);
    }
}
