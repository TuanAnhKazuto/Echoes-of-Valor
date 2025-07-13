using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject bossPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossPrefab.SetActive(true);
        }
    }
}
