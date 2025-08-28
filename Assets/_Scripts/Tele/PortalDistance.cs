using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Cổng dịch chuyển")]
    public Transform targetPortal;


    private void OnTriggerEnter(Collider other)
    {
        // chỉ dịch chuyển Player
        if (other.CompareTag("Player"))
        {
            Transform player = other.transform;

            CharacterController controller = other.GetComponent<CharacterController>();
            controller.enabled = false;

            Vector3 targetPos = new(targetPortal.position.x, targetPortal.position.y, targetPortal.position.z);

            player.position = targetPos;
            player.rotation = targetPortal.rotation;

            other.transform.position = targetPos;
            controller.enabled = true;

            Debug.Log("Teleport tới: " + targetPortal.name);
        }
    }
}
