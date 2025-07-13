using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject hpPrefab;
    public GameObject mpPrefab;
    public GameObject expPrefab;
    public GameObject corPrefab;
    public Transform spawnPoint;
    Animator animator;

    [Header("Quest")]
    public string questTag = "Enemy_Main";

    private bool isOpened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            isOpened = true;
            animator.SetBool("Open", true);
            Invoke(nameof(OpenChest), 1f);
            Destroy(gameObject, 3f);
        }
    }

    void OpenChest()
    {
        List<GameObject> itemList = new List<GameObject> { hpPrefab, mpPrefab, expPrefab, corPrefab };

        for (int i = 0; i < 2; i++)
        {
            if (itemList.Count == 0) break;

            int randomIndex = Random.Range(0, itemList.Count);
            GameObject selectedItem = itemList[randomIndex];

            DropItem(selectedItem);
            itemList.RemoveAt(randomIndex);
        }
    }

    void DropItem(GameObject itemPrefab)
    {
        Vector3 spawnPos = spawnPoint.position;
        GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Hướng trước mặt rương (có thể chỉnh độ lệch nhỏ)
            Vector3 forward = transform.forward.normalized;
            Vector3 force = forward * Random.Range(4f, 6f) + Vector3.up * Random.Range(4f, 6f);
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
