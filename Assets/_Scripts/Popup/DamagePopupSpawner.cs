using UnityEngine;

public class DamagePopupSpawner : MonoBehaviour
{
    public static DamagePopupSpawner Instance;
    public GameObject damagePopupPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowDamage(Vector3 worldPosition, int damage, Color color)
    {
        if (damagePopupPrefab == null)
        {
            Debug.LogWarning("Damage Popup Prefab chưa được gán!");
            return;
        }

        GameObject popup = Instantiate(damagePopupPrefab, worldPosition, Quaternion.identity);
        popup.GetComponent<DamagePopup>().Setup(damage, color);
    }
}
