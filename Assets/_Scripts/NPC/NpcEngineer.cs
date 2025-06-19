using UnityEngine;

public class NpcEngineer : MonoBehaviour
{
    public GameObject upgradeWeaponPanel;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            upgradeWeaponPanel.SetActive(true);
            Cursor.lockState= CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
