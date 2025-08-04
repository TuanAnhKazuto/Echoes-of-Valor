using UnityEngine;

public class SwithPanelController : MonoBehaviour
{
    public GameObject[] panels;

    private void Start()
    {
        panels[0].SetActive(true); 
        panels[1].SetActive(false);
    }

    public void SwitchToPanel(int panelIndex)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); // Tắt tất cả các panel
        }
        panels[panelIndex].SetActive(true); // Bật panel được chọn
    }
}
