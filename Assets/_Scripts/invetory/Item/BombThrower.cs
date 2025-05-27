using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bombPrefab;           // Prefab quả bom
    public Transform throwPoint;            // Vị trí ném (nên là vị trí trước mặt camera/player)
    public float throwForce = 10f;          // Lực ném

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
            rb.linearVelocity = throwPoint.forward * throwForce;
        }
    }
}
