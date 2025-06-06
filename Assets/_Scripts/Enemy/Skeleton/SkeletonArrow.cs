using UnityEngine;

public class SkeletonArrow : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;
    private float arrowRotationX;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.up * -speed * Time.deltaTime;
    }
}
