using TMPro;
using UnityEngine;

public class TabsController : MonoBehaviour
{
    public GameObject[] tabs;
    public GameObject[] weaponObj;

    private void Start()
    {
        tabs[0].SetActive(true);
        tabs[1].SetActive(false);

        if (weaponObj[0] != null && weaponObj[1] != null)
        {
            weaponObj[0].SetActive(true);
            weaponObj[1].SetActive(false);
        }
    }

    public void SwitchToTab(int tabIndex)
    {
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false); // Tắt tất cả các tab
            
        }
        tabs[tabIndex].SetActive(true); // Bật tab được chọn

        foreach (GameObject weapon in weaponObj)
        {
            weapon.SetActive(false); // Tắt tất cả các vũ khí
        }
        weaponObj[tabIndex].SetActive(true); // Bật vũ khí tương ứng với tab được chọn
    }
}
