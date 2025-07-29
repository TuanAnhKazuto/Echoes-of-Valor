using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ItemDrop : MonoBehaviour
{
    [Header("Di chuyển về phía người chơi")]
    private NavMeshAgent nav;
    private Transform player;

    [Header("Giá trị Echo")]
    public int echoValue = 1;

    [Header("Hiển thị số lượng Echo")]
    public TextMeshProUGUI echoText;
    private static int totalEcho = 0;

    private void Start()
    {
        // Lấy NavMeshAgent
        nav = GetComponent<NavMeshAgent>();

        // Tìm người chơi theo tag
        player = GameObject.FindWithTag("Player")?.transform;

        // Tìm UI nếu chưa gán
        if (echoText == null)
        {
            echoText = GameObject.Find("TextEcho")?.GetComponent<TextMeshProUGUI>();
        }

        UpdateEchoUI();
    }

    private void Update()
    {
        if (player != null && nav != null)
        {
            nav.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            totalEcho += echoValue;
            UpdateEchoUI();

            Debug.Log("Đã nhặt Echo! Tổng: " + totalEcho);
            Destroy(gameObject);
        }
    }

    private void UpdateEchoUI()
    {
        if (echoText != null)
        {
            echoText.text = "Echo: " + totalEcho;
        }
    }
}