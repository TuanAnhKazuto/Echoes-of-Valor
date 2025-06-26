using UnityEngine;

public class NpcEngineer : MonoBehaviour
{
    public GameObject upgradeWeaponPanel;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            Invoke(nameof(ShowPanel), 1f);
        }
    }

    private void ShowPanel()
    {
        upgradeWeaponPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
