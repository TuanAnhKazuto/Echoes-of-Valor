using UnityEngine;
using UnityEngine.UI;
using static ShopManager;

public class ShopUIController : MonoBehaviour
{
    public Transform shopContentParent;
    public GameObject shopItemPrefab;
    public GameObject shopPanel; 
    public Image itemImage;

    private bool isShopOpen = false;

    void Start()
    {
        LoadShopItems();
        if (shopPanel != null) shopPanel.SetActive(false); // Ẩn shop khi bắt đầu
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isShopOpen = !isShopOpen;
            shopPanel.SetActive(isShopOpen);

            if (isShopOpen)
            {
                LoadShopItems();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    } 
    public void SetShopActive(bool isActive)
    {
        isShopOpen = isActive;
        if (shopPanel != null)
            shopPanel.SetActive(isActive);

        if (isActive)
            LoadShopItems();
    }
    void LoadShopItems()
    {
        if (ShopManager.Instance == null)
        {
            Debug.LogError("❌ ShopManager.Instance chưa tồn tại trong Scene!");
            return;
        }
        if (shopItemPrefab == null)
        {
            Debug.LogError("❌ shopItemPrefab chưa được kéo vào ShopUIController!");
            return;
        }
        if (shopContentParent == null)
        {
            Debug.LogError("❌ shopContentParent chưa được kéo vào ShopUIController!");
            return;
        }

        foreach (Transform child in shopContentParent)
            Destroy(child.gameObject);

        foreach (ShopItemProduct product in ShopManager.Instance.products)
        {
            GameObject go = Instantiate(shopItemPrefab, shopContentParent);
            ShopItemUI ui = go.GetComponent<ShopItemUI>();
            if (ui != null)
                ui.SetupUI(product);
            else
                Debug.LogError("❌ Prefab shopItemPrefab không có ShopItemUI!");
        }
    }


}
