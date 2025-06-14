using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    public Slider healthSlider;
    public Canvas canvas;

    [Header("Target & Offset")]
    public Transform target; // Nhân vật cần theo dõi
    public Vector3 offset = new Vector3(0, 2f, 0); // Vị trí trên đầu

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        canvas.worldCamera = mainCam;
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(float currentHealth)
    {
        healthSlider.value = Mathf.Clamp(currentHealth, 0f, healthSlider.maxValue);
    }

    void LateUpdate()
    {
        if (target == null || mainCam == null) return;

        // Cập nhật vị trí thanh máu theo nhân vật
        transform.position = target.position + offset;

        // Hướng về camera
        Vector3 dir = transform.position - mainCam.transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}