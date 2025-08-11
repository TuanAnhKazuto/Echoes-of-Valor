using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopViewPanelCtrl : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI pricePerItemText;
    public TextMeshProUGUI totalPriceText;
    public TextMeshProUGUI stockText;

    public ShopManager.ShopItemProduct selectedProduct;
    public ShopCtrlUI shopCtrl;

    public void SetupProduct(ShopManager.ShopItemProduct product, int stockQuantity)
    {
        selectedProduct = product;

        itemIcon.sprite = product.itemData.image;
        itemNameText.text = product.itemData.itemName;
        pricePerItemText.text = product.price.ToString();
        stockText.text = stockQuantity > 0
            ? $"<color=#FFFFFF>{stockQuantity}</color>"
            : $"<color=#FF0000>{stockQuantity}</color>";

        shopCtrl.SetupMaxQuantity(product.maxQuantity);
        shopCtrl.ShowUI();
    }

    public void UpdateTotalPriceText(int totalPrice)
    {
        totalPriceText.text = totalPrice.ToString();
    }

    public void ClearView()
    {
        selectedProduct = null;
        itemIcon.sprite = null;
        itemNameText.text = "";
        pricePerItemText.text = "0";
        totalPriceText.text = "0";
        stockText.text = "";
        shopCtrl.HideUI();
    }
    public void SetSelectedProduct(ShopManager.ShopItemProduct product)
    {
        if (product != null)
        {
            // Ở đây bạn có thể lấy stock từ inventory hoặc mặc định maxQuantity
            SetupProduct(product, product.maxQuantity);
        }
    }
}
