using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShopManager;


public class ShopItemUI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;

    private ShopItemProduct product;

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
        Debug.Log("Chọn sản phẩm: " + product.itemData.itemName);
    }
}
