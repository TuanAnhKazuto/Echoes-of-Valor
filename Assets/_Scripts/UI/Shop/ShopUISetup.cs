using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUISetup : MonoBehaviour
{
    [System.Serializable]
    public class ShopSlot
    {
        public Image icon;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;
        public Button buyButton;
    }

    [Header("Shop Slot Setup")]
    public ShopSlot[] shopSlots; // các ô hiển thị sản phẩm

    private void Start()
    {
        LoadShopItems();
    }

    void LoadShopItems()
    {
        var products = ShopManager.Instance.products;

        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (i < products.Count)
            {
                var product = products[i];

                // Gán dữ liệu vào UI
                shopSlots[i].icon.sprite = product.itemData.image; // hoặc icon nếu dùng Ingredient
                shopSlots[i].nameText.text = product.itemData.itemName;
                shopSlots[i].priceText.text = product.price + " Gold";

                // Gán nút mua
                int quantity = 1; // mặc định mua 1
                shopSlots[i].buyButton.onClick.RemoveAllListeners();
                shopSlots[i].buyButton.onClick.AddListener(() =>
                {
                    ShopManager.Instance.BuyItem(product, quantity);
                });

                shopSlots[i].icon.gameObject.SetActive(true);
            }
            else
            {
                // Ẩn ô thừa
                shopSlots[i].icon.gameObject.SetActive(false);
                shopSlots[i].nameText.text = "";
                shopSlots[i].priceText.text = "";
                shopSlots[i].buyButton.onClick.RemoveAllListeners();
            }
        }
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadShopItems();
    }
}
