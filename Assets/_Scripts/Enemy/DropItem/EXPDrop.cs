using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EXPDrop : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;

    [Header("Giá trị EXP của hạt này")]
    public int expValue = 1;

    [Header("UI EXP")]
    public TextMeshProUGUI expText;
    private static int totalExp = 0;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;

        // Tìm UI EXP nếu chưa gán
        if (expText == null)
        {
            expText = GameObject.Find("TextExp")?.GetComponent<TextMeshProUGUI>();
        }

        UpdateExpUI();
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
            totalExp += expValue;
            UpdateExpUI();

            Debug.Log("Đã nhặt EXP! Tổng: " + totalExp);
            Destroy(gameObject);
        }
    }

    private void UpdateExpUI()
    {
        if (expText != null)
        {
            expText.text = "EXP: " + totalExp;
        }
    }
}
