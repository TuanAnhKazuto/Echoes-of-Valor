using UnityEngine;
using UnityEngine.AI;

public class ItemDrop : MonoBehaviour
{
    NavMeshAgent nav;

    public float speed;

    public Transform player;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        nav.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Thêm hàm nhận vật phẩm vào đây;

            Debug.Log("Nhat.!!!!!!!!!!!!!!!");
            Destroy(gameObject);
        }
    }
}