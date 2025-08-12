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

    public static ShopManager Instance;

    [Header("Danh sách sản phẩm bán trong shop (Item)")]
    public List<ShopItemProduct> products = new List<ShopItemProduct>();

    private Cor playerCor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        playerCor = FindAnyObjectByType<Cor>(); // Lấy vàng từ Cor
    }

    public bool BuyItem(ShopItemProduct product, int quantity)
    {
        int totalCost = product.price * quantity;

        // Kiểm tra đủ vàng
        if (playerCor != null && playerCor.cor >= totalCost)
        {
            // Trừ vàng
            playerCor.cor -= totalCost;

            // Cập nhật UI vàng
            if (playerCor.coin != null)
                playerCor.coin.text = playerCor.cor.ToString();

            // Thêm vào inventory
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
}


