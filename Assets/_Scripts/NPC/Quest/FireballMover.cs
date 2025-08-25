using System.Collections.Generic;
using UnityEngine;

public class FireballMover : MonoBehaviour
{
    private List<Vector3> pathPoints = new List<Vector3>();
    private int currentIndex = 0;
    public float moveSpeed = 50f;
    public float playerActivateDistance = 5f; // khoảng cách player phải gần mới chạy
    public float heightOffset = 2f;

    private Transform player;
    private bool reachedEnd = false;

    // Khởi tạo đường đi từ Player đến Target
    public void InitPath(Transform player, Transform target, float spacing = 5f, int maxPoints = 30)
    {
        pathPoints.Clear();
        currentIndex = 0;
        reachedEnd = false;

        this.player = player;

        if (player == null || target == null) return;

        Vector3 start = player.position;
        Vector3 end = target.position;

        float distance = Vector3.Distance(start, end);
        int count = Mathf.Min(Mathf.CeilToInt(distance / spacing), maxPoints);

        Vector3 dir = (end - start).normalized;

        for (int i = 1; i <= count; i++)
        {
            Vector3 pos = start + dir * (i * spacing);

            // bám sát mặt đất
            if (Physics.Raycast(pos + Vector3.up * 5, Vector3.down, out RaycastHit hit, 20f))
                pos = hit.point + Vector3.up * heightOffset;

            pathPoints.Add(pos);
        }

        if (pathPoints.Count > 0)
            transform.position = pathPoints[0];
    }

    void Update()
    {
        if (pathPoints.Count == 0 || player == null || reachedEnd) return;

        // 🔹 chỉ di chuyển khi player ở gần
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        if (distToPlayer > playerActivateDistance) return;

        Vector3 targetPos = pathPoints[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // xoay fireball theo hướng đi
        Vector3 dir = (targetPos - transform.position).normalized;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        if (Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            currentIndex++;
            if (currentIndex >= pathPoints.Count)
            {
                reachedEnd = true;
                Destroy(gameObject); // 🔥 tới nơi thì biến mất
            }
        }
    }
}
