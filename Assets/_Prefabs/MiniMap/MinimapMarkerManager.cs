using UnityEngine;
using System.Collections.Generic;

public class MinimapMarkerManager : MonoBehaviour
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
    public List<Transform> targets;

    private List<Marker> markers = new List<Marker>();

    void Start()
    {
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
