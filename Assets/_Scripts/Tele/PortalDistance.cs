using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Cổng dịch chuyển")]
    public Transform targetPortal;
    public float offsetForward = 2f;  // dịch ra trước cổng
    public float offsetUp = 1f;       // nhấc player lên

    private void OnTriggerEnter(Collider other)
    {
        // chỉ dịch chuyển Player
        if (other.CompareTag("Player"))
        {
            Transform player = other.transform;

            Vector3 targetPos = targetPortal.position
                                + targetPortal.forward * offsetForward
                                + Vector3.up * offsetUp;

            player.position = targetPos;
            player.rotation = targetPortal.rotation;

            Debug.Log("Teleport tới: " + targetPortal.name);
        }
    }
}
