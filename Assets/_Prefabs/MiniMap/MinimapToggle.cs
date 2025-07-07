using UnityEngine;

public class MinimapToggle : MonoBehaviour
{
    public GameObject smallMapUI;   // Panel nhỏ
    public GameObject bigMapUI;     // Panel lớn
    public MinimapCameraController cameraController;

    private bool isBigMap = false;

    void Start()
    {
        smallMapUI.SetActive(true);
        bigMapUI.SetActive(false);
        cameraController.SetBigMapMode(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isBigMap = !isBigMap;
            smallMapUI.SetActive(!isBigMap);
            bigMapUI.SetActive(isBigMap);
            cameraController.SetBigMapMode(isBigMap);
        }
    }
}
