using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject hpPrefab;
    public GameObject mpPrefab;
    public GameObject expPrefab;
    public Transform spawnPoint;

    private bool isOpened = false;

    void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            isOpened = true;
            OpenChest();
        }
    }

    void OpenChest()
    {
        DropItem(hpPrefab);
        DropItem(mpPrefab);
        DropItem(expPrefab);
    }

    void DropItem(GameObject itemPrefab)
    {
        Vector3 spawnPos = spawnPoint.position;
        GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Bay lên và lệch sang trái/phải
            Vector3 force = new Vector3(Random.Range(-1f, 5f), Random.Range(5f, 7f), Random.Range(-1f, 5f));
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
