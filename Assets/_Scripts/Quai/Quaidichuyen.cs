using UnityEngine;

public class Quaidichuyen : MonoBehaviour
{
    public Transform player;         // Người chơi
    public float moveSpeed = 3f;     // Tốc độ di chuyển
    public float chaseRange = 10f;   // Khoảng cách phát hiện

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < chaseRange)
        {
            // Tính hướng đến người chơi
            Vector3 direction = (player.position - transform.position).normalized;

            // Di chuyển tới người chơi
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Quay mặt theo hướng di chuyển
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
        }
    }
}