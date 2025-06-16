using UnityEngine;

public class TabsSwitchManger : MonoBehaviour
{
    public GameObject[] tabs;
    public GameObject[] weaponObj;

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
