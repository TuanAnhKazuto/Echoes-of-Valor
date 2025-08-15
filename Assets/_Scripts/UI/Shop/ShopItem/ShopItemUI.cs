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
    private ShopIngredientProduct currentIngredient;

    private void Start()
    {
        shopUIController = FindAnyObjectByType<ShopUIController>();
    }
    public void SetupUI(ShopItemProduct _product)
    {
        product = _product;
        currentIngredient = null;

        itemIcon.sprite = product.itemData.image;
        itemNameText.text = product.itemData.itemName;
        priceText.text = product.price.ToString();
    }
    // Hiển thị sản phẩm Ingredient
    public void SetupIngredientUI(ShopIngredientProduct product)
    {
        currentIngredient = product;       

        itemIcon.sprite = product.ingredientData.icon;
        itemNameText.text = product.ingredientData.ingredientName;
        priceText.text = product.price.ToString();
    }
    public void OnSelect()
    {
        
        shopUIController.itemImage.sprite = itemIcon.sprite;
        
        ShopViewPanelCtrl viewPanel = FindAnyObjectByType<ShopViewPanelCtrl>();
        if (viewPanel != null)
        {
            if (currentIngredient != null) 
            {
                //viewPanel.SetupIngredient(currentIngredient, currentIngredient.maxQuantity);
                Debug.Log("Chọn nguyên liệu: " + currentIngredient.ingredientData.ingredientName);
            }
            else if (product != null) 
            {
                viewPanel.SetupProduct(product, product.maxQuantity);
                Debug.Log("Chọn sản phẩm: " + product.itemData.itemName);
            }
        }

    }
    public void SetupIngredient(ShopIngredientProduct ingredientProduct, int quantity)
    {

    }

}
