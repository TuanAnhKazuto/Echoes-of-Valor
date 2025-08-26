using System.Collections.Generic;
using UnityEngine;

public class FireballMover : MonoBehaviour
{
    private List<Vector3> pathPoints = new List<Vector3>();
    private int currentIndex = 0;
    public float moveSpeed = 50f;
    public float playerActivateDistance = 5f; 
    public float heightOffset = 2f;
    private float hoverOffset = 0f; 

    private Transform player;
    private bool reachedEnd = false;
    private Vector3 basePosition; 

   
    [Header("Hiệu ứng khi đứng yên & khi bay")]
    public float hoverAmplitude = 0.5f;  
    public float hoverFrequency = 2f;    

    
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

            
            if (Physics.Raycast(pos + Vector3.up * 5, Vector3.down, out RaycastHit hit, 20f))
                pos = hit.point + Vector3.up * heightOffset;

            pathPoints.Add(pos);
        }

        if (pathPoints.Count > 0)
        {
            transform.position = pathPoints[0];
            basePosition = transform.position; 
        }
    }

    void Update()
    {
        if (pathPoints.Count == 0 || player == null || reachedEnd) return;

        float distToPlayer = Vector3.Distance(transform.position, player.position);
        if (distToPlayer > playerActivateDistance)
        {
            Vector3 hoverPos = transform.position;
            hoverPos.y = basePosition.y + Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
            transform.position = hoverPos;
            return;
        }      

        Vector3 targetPos = pathPoints[currentIndex];        
        targetPos.y += Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);        
        Vector3 dir = (targetPos - transform.position).normalized;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
        if (Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            currentIndex++;
            if (currentIndex >= pathPoints.Count)
            {
                reachedEnd = true;
                Destroy(gameObject); 
            }
        }
    }
}
