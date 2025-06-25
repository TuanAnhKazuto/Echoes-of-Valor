using Unity.VisualScripting;
using UnityEngine;

public class ArrowArm2Player : MonoBehaviour
{
    public PlayerController player;

    private void Update() 
    {
        Vector3 direction = (player.targetForCam.position - transform.position).normalized;
        direction.x = -90; 
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
