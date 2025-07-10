using UnityEngine;

public class MinimapToggle : MonoBehaviour
{
    [Header("UI Minimap")]
    public GameObject smallMapUI;        // Panel nhỏ
    public GameObject bigMapUI;          // Panel lớn
    public GameObject canvasMark;        // Canvas chứa Image marker (CanvasMark)

    [Header("Camera Controller")]
    public MinimapCameraController cameraController; // script camera
    public CameraMiniMapFollow cameraFollowScript;   // script follow (tắt/mở khi zoom lớn)

    private bool isBigMap = false;

    void Start()
    {
        smallMapUI.SetActive(true);
        bigMapUI.SetActive(false);

        if (canvasMark != null)
            canvasMark.SetActive(true);

        if (cameraFollowScript != null)
            cameraFollowScript.enabled = true;

        if (cameraController != null)
            cameraController.SetBigMapMode(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isBigMap = !isBigMap;

            // Bật/tắt UI panel
            smallMapUI.SetActive(!isBigMap);
            bigMapUI.SetActive(isBigMap);

            // Bật/tắt chế độ di chuyển camera thủ công
            if (cameraController != null)
                cameraController.SetBigMapMode(isBigMap);

            // Bật/tắt follow camera
            if (cameraFollowScript != null)
                cameraFollowScript.enabled = !isBigMap;

            // Bật/tắt canvasMark theo chế độ bản đồ
            if (canvasMark != null)
                canvasMark.SetActive(!isBigMap);
        }
    }
}
