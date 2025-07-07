using UnityEngine;

public class CameraMiniMapFollow : MonoBehaviour
{
    private Transform target; 
    public Vector3 offset = new Vector3(0, 20, 0); 
    void LateUpdate()
    {
        if (target == null)
        {
         
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
