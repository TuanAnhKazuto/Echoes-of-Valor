using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject hpPrefab;
    public GameObject mpPrefab;
    public GameObject expPrefab;
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
            //OpenChest();
            animator.SetBool("Open", true);
            Invoke(nameof(OpenChest), 0.5f);
            Destroy(gameObject, 5f);
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
