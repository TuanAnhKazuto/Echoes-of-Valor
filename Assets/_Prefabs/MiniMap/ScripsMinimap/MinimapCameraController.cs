using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    public static bool IsBigMapMode = false;

    [Header("Player Follow")]
    public Transform player;
    public Vector3 followOffset = new Vector3(0, 20, 0);

    [Header("Big Map Settings")]
    public float panSpeed = 50f;
    public float zoomSpeed = 500f;
    public float minZoom = 10f;
    public float maxZoom = 200f;
    public float mousePanSpeed = 1.5f;

    private bool isBigMapMode = false;
    private Camera cam;
    private Vector3 lastMousePosition;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Tìm player nếu chưa gán
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Góc nhìn từ trên xuống
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void LateUpdate()
    {
        if (!isBigMapMode)
        {
            if (player != null)
            {
                transform.position = player.position + followOffset;
            }
        }
        else
        {
            HandleKeyboardPan();
            HandleMousePan();
            HandleZoom();
        }
    }

    void HandleKeyboardPan()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) move += Vector3.back;
        if (Input.GetKey(KeyCode.A)) move += Vector3.left;
        if (Input.GetKey(KeyCode.D)) move += Vector3.right;

        transform.Translate(move * panSpeed * Time.unscaledDeltaTime, Space.World);
    }

    void HandleMousePan()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, 0, -delta.y) * mousePanSpeed * Time.unscaledDeltaTime;
            transform.Translate(move, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed * Time.unscaledDeltaTime;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }

    /// <summary>
    /// Bật/tắt bản đồ lớn
    /// </summary>
    /// <param name="isBig">true nếu bật</param>
    public void SetBigMapMode(bool isBig)
    {
        isBigMapMode = isBig;
        IsBigMapMode = isBig;

        // Tạm dừng hoặc tiếp tục game
        Time.timeScale = isBig ? 0f : 1f;

        // Chuột
        Cursor.visible = isBig;
        Cursor.lockState = isBig ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
