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
        // Tạo danh sách vật phẩm
        List<GameObject> itemList = new List<GameObject> { hpPrefab, mpPrefab, expPrefab, corPrefab };

        // Random 2 vật phẩm khác nhau
        for (int i = 0; i < 2; i++)
        {
            if (itemList.Count == 0) break;

            int randomIndex = Random.Range(0, itemList.Count);
            GameObject selectedItem = itemList[randomIndex];

            DropItem(selectedItem);

            itemList.RemoveAt(randomIndex); // Xóa khỏi danh sách để không trùng
        }
    }

    void DropItem(GameObject itemPrefab)
    {
        Vector3 spawnPos = spawnPoint.position;
        GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 force = new Vector3(Random.Range(-1f, 5f), Random.Range(5f, 7f), Random.Range(-1f, 5f));
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
