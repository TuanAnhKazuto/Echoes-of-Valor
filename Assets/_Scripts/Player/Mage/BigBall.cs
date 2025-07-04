using UnityEngine;

public class BigBall : MonoBehaviour
{
    public float speed = 10f;
    public float explosionDelay = 1.5f;
    public LayerMask enemyLayer;

    public float[] radii = { 2f, 4f, 6f };
    public float[] damages = { 250f, 120f, 60f };

    public GameObject explosionVFX; 

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

            foreach (var enemyCollider in enemies)
            {
                EnemyStats enemy = enemyCollider.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damages[i]);
                    Vector3 popupPos = enemy.transform.position + Vector3.up * 2f;
                    DamagePopupSpawner.Instance.ShowDamage(popupPos, (int)damages[i], Color.red);
                }
            }
        }

        if (explosionVFX != null)
        {
            GameObject vfx = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(vfx, 3f);
        }


        Destroy(gameObject);
    }
}
