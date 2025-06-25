using UnityEngine;

public class SkeletonArrow : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;
    private float arrowRotationX;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        //Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        //transform.LookAt(player.position);
    }

    private void Update()
    {
        transform.position += -speed * Time.deltaTime * transform.up;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
