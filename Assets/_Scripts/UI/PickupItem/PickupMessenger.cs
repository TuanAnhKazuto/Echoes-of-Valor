using UnityEngine;

public class PickupMessenger : MonoBehaviour
{
    public GameObject messegerPickup;

    public void ShowPickupMessage()
    {
        GameObject messeger  = Instantiate(messegerPickup, transform);
    }
}
