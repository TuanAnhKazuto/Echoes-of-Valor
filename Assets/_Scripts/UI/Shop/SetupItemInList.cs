using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ShopManager;

public class SetupItemInList : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI priceText;
    public ShopItemProduct productData;

    public void Setup(ShopItemProduct product)
    {
        if (product == null) return;
        productData = product;

        itemIcon.sprite = product.itemData.image;
        itemName.text = product.itemData.itemName;
        priceText.text = product.price.ToString();
    }
}
