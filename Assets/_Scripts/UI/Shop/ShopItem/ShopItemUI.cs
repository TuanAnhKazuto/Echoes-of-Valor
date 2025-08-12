using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShopManager;


public class ShopItemUI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    public ShopUIController shopUIController;

    private ShopItemProduct product;

    private void Start()
    {
        shopUIController = FindAnyObjectByType<ShopUIController>();
    }
    public void SetupUI(ShopItemProduct _product)
    {
        product = _product;
        itemIcon.sprite = product.itemData.image;
        itemNameText.text = product.itemData.itemName;
        priceText.text = product.price.ToString();
    }

    public void OnSelect()
    {
        // Gửi sản phẩm sang panel mua chi tiết
        shopUIController.itemImage.sprite = itemIcon.sprite;
        Debug.Log("Chọn sản phẩm: " + product.itemData.itemName);
    }
}
