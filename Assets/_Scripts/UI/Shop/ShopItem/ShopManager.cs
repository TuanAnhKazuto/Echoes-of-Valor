using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopItemProduct
    {
        public Item itemData;
        public int price;
        public int maxQuantity = 99;
    }

    [System.Serializable]
    public class ShopIngredientProduct
    {
        public Ingredient ingredientData;
        public int price;
        public int maxQuantity = 99;
    }
    public static ShopManager Instance;

    [Header("Danh sách sản phẩm bán trong shop (Item)")]
    public List<ShopItemProduct> products = new List<ShopItemProduct>();

    [Header("Danh sách nguyên liệu bán trong shop (Ingredient)")]
    public List<ShopIngredientProduct> ingredientProducts = new List<ShopIngredientProduct>();

    private Cor playerCor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        playerCor = FindAnyObjectByType<Cor>(); 
    }

    public bool BuyItem(ShopItemProduct product, int quantity)
    {
        int totalCost = product.price * quantity;

        
        if (playerCor != null && playerCor.cor >= totalCost)
        {           
            playerCor.cor -= totalCost;
            
            if (playerCor.corText != null)
                playerCor.corText.text = playerCor.cor.ToString();

            
            ShopUIController shopUI = FindAnyObjectByType<ShopUIController>();
            if (shopUI != null)
                shopUI.UpdateGoldUI();
            
            for (int i = 0; i < quantity; i++)
            {
                InventoryManager.Instance.Add(product.itemData);
            }

            Debug.Log($"Mua {quantity} x {product.itemData.itemName} thành công!");
            return true;
        }
        else
        {
            Debug.Log("Không đủ vàng để mua!");
            return false;
        }
    }

    public bool BuyIngredient(ShopIngredientProduct product, int quantity)
    {
        int totalCost = product.price * quantity;

        if (playerCor != null && playerCor.cor >= totalCost)
        {
            playerCor.cor -= totalCost;

            if (playerCor.corText != null)
                playerCor.corText.text = playerCor.cor.ToString();

            ShopUIController shopUI = FindAnyObjectByType<ShopUIController>();
            if (shopUI != null)
                shopUI.UpdateGoldUI();

            for (int i = 0; i < quantity; i++)
            {
                InventoryManager.Instance.AddIngredients(product.ingredientData, 1);
            }

            Debug.Log($"Mua {quantity} x {product.ingredientData.ingredientName} thành công!");
            return true;
        }
        else
        {
            Debug.Log("Không đủ vàng để mua nguyên liệu!");
            return false;
        }
    }
}


