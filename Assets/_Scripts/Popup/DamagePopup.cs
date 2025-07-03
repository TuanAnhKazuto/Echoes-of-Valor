using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float floatSpeed = 1f;
    public float disappearTime = 1f;

    private CanvasGroup canvasGroup;
    private float elapsedTime = 0f;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        elapsedTime += Time.deltaTime;
        canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / disappearTime);

        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0);

        if (elapsedTime >= disappearTime)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int damage, Color color)
    {
        damageText.text = damage.ToString();
        damageText.color = color;
    }
}
