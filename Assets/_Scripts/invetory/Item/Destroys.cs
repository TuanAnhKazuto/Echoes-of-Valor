using UnityEngine;

public class Destroys : MonoBehaviour
{
    public float lifetime = 20f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
