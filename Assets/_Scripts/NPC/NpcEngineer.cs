using UnityEngine;

public class NpcEngineer : MonoBehaviour
{
    public GameObject upgradeWeaponPanel;
    WeaponUI3DSetup weaponUI3DSetup;

    public bool isPlayerInRange = false;

    private void Start()
    {
        weaponUI3DSetup = upgradeWeaponPanel.GetComponent<WeaponUI3DSetup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Hiển thị thông báo "Nhấn F để nâng cấp vũ khí" (nếu có)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Ẩn thông báo "Nhấn F để nâng cấp vũ khí" (nếu có)
        }
    }

    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if(!weaponUI3DSetup.isPanelOpen)
            {
                Invoke(nameof(ShowPanel), 1f);
            }
        }
    }

    private void ShowPanel()
    {
        upgradeWeaponPanel.SetActive(true);
        weaponUI3DSetup.isPanelOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
