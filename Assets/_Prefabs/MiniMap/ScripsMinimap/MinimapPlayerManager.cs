using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerManager : MonoBehaviour
{
    [System.Serializable]
    public class Marker
    {
        public Transform target;
        public RectTransform smallIcon;
        public RectTransform bigIcon;
    }

    [Header("Camera minimap")]
    public Camera minimapCamera;

    [Header("Minimap nhỏ")]
    public RectTransform smallMapPanel;
    public GameObject smallIconPrefab;

    [Header("Minimap lớn")]
    public RectTransform bigMapPanel;
    public GameObject bigIconPrefab;

    [Header("Danh sách NPC / điểm cần đánh dấu")]
    public List<Transform> targets = new List<Transform>(); // vẫn cho phép gán NPC nếu muốn

    private List<Marker> markers = new List<Marker>();

    void Start()
    {
        // Tìm Player tự động
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Transform playerTransform = playerObj.transform;

            // Chỉ thêm nếu chưa có
            if (!targets.Contains(playerTransform))
            {
                targets.Insert(0, playerTransform); // Cho player lên đầu danh sách nếu muốn
            }
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Player với tag 'Player'");
        }

        // Tạo icon cho mỗi target
        foreach (Transform target in targets)
        {
            GameObject iconSmall = Instantiate(smallIconPrefab, smallMapPanel);
            GameObject iconBig = Instantiate(bigIconPrefab, bigMapPanel);

            markers.Add(new Marker
            {
                target = target,
                smallIcon = iconSmall.GetComponent<RectTransform>(),
                bigIcon = iconBig.GetComponent<RectTransform>()
            });
        }
    }

    void Update()
    {
        foreach (var m in markers)
        {
            if (m.target == null || minimapCamera == null) continue;

            Vector3 viewportPos = minimapCamera.WorldToViewportPoint(m.target.position);
            bool isVisible = viewportPos.z > 0;

            // ----------------- Minimap nhỏ -----------------
            if (m.smallIcon != null && smallMapPanel != null)
            {
                m.smallIcon.gameObject.SetActive(isVisible);
                if (isVisible)
                {
                    Vector2 anchoredPos = new Vector2(
                        (viewportPos.x - 0.5f) * smallMapPanel.sizeDelta.x,
                        (viewportPos.y - 0.5f) * smallMapPanel.sizeDelta.y
                    );

                    float halfWidth = smallMapPanel.sizeDelta.x / 2f;
                    float halfHeight = smallMapPanel.sizeDelta.y / 2f;
                    anchoredPos.x = Mathf.Clamp(anchoredPos.x, -halfWidth, halfWidth);
                    anchoredPos.y = Mathf.Clamp(anchoredPos.y, -halfHeight, halfHeight);

                    m.smallIcon.anchoredPosition = anchoredPos;
                }
            }

            // ----------------- Bản đồ lớn -----------------
            if (m.bigIcon != null && bigMapPanel != null)
            {
                m.bigIcon.gameObject.SetActive(isVisible);
                if (isVisible)
                {
                    Vector2 anchoredPos = new Vector2(
                        (viewportPos.x - 0.5f) * bigMapPanel.sizeDelta.x,
                        (viewportPos.y - 0.5f) * bigMapPanel.sizeDelta.y
                    );

                    float halfWidth = bigMapPanel.sizeDelta.x / 2f;
                    float halfHeight = bigMapPanel.sizeDelta.y / 2f;
                    anchoredPos.x = Mathf.Clamp(anchoredPos.x, -halfWidth, halfWidth);
                    anchoredPos.y = Mathf.Clamp(anchoredPos.y, -halfHeight, halfHeight);

                    m.bigIcon.anchoredPosition = anchoredPos;
                }
            }
        }
    }
}
