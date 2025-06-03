using UnityEngine;

public class BigBall : MonoBehaviour
{
    public float speed = 10f;
    public float explosionDelay = 1.5f;
    public LayerMask enemyLayer;

    public float[] radii = { 2f, 4f, 6f };
    public float[] damages = { 250f, 170f, 100f };

    private void Start()
    {
        Invoke(nameof(Explode), explosionDelay);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Explode()
    {
        for (int i = 0; i < radii.Length; i++)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, radii[i], enemyLayer);
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Enemy>()?.TakeDamage(damages[i]);
            }
        }


        Destroy(gameObject);
    }
}
