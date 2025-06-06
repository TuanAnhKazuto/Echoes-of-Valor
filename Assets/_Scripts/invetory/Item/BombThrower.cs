using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bombPrefab;    
    public Transform throwPoint;       
    public float throwForce = 10f;        

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowBomb();
        }
    }

    void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = throwPoint.forward * throwForce;
        }
    }
}
