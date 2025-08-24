using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [System.Serializable]
    public class InventoryItem
    {
        public Item item;
        public int quantity;
        public string description;

        public InventoryItem(Item item, int quantity, string description)
        {
            this.item = item;
            this.quantity = quantity;
            this.description = description;
        }
    }

    public List<InventoryItem> items = new List<InventoryItem>();

    public Transform itemContentPanel;
    public GameObject itemPrefab;

    // Upgrade Ingredients
    [System.Serializable]
    public class UpgradeIngredient
    {
        public Ingredient ingredient;
        public int quantity;
        public string description;

        public UpgradeIngredient(Ingredient ingredient, int quantity, string description)
        {
            this.ingredient = ingredient;
            this.quantity = quantity;
            this.description = description;
        }
    }

    public List<UpgradeIngredient> upgradeIngredients = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);

        // dong lay
        if (itemContentPanel == null)
        {
            itemContentPanel = GameObject.Find("Content").transform;

        }
    }

    public void Add(Item item)
    {
        InventoryItem existingItem = items.Find(i => i.item.id == item.id);
        //InventoryItem exstingItemtype = items.Find(i => i.item.itemType == item.itemType);

        if (item.itemType == ItemType.Cor)
        {
            return;
        }

        if (existingItem != null)
        {
            existingItem.quantity++;
        }
        else
        {
            items.Add(new InventoryItem(item, 1, item.description));
        }
        
        DisplayInventory();
    }

    public void Remove(Item item)
    {
        InventoryItem existingItem = items.Find(i => i.item.id == item.id);
        if (existingItem != null)
        {
            existingItem.quantity--;
            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
        }
    }

    // Thêm nguyên liệu nâng cấp vào túi đồ
    public void AddIngredients(Ingredient ingredient, int count)
    {
        UpgradeIngredient upgradeIngredient = upgradeIngredients.Find(i => i.ingredient.ingredientsID == ingredient.ingredientsID);

        if (upgradeIngredient != null)
        {
            upgradeIngredient.quantity += count;
        }
        else
        {
            upgradeIngredients.Add(new UpgradeIngredient(ingredient, count, ingredient.description));
        }
    }

    public void RemoveIngredients(Ingredient ingredient, int count)
    {
        UpgradeIngredient upgradeIngredient = upgradeIngredients.Find(i => i.ingredient.ingredientsID == ingredient.ingredientsID);
        if (upgradeIngredient != null)
        {
            upgradeIngredient.quantity -= count;
            if (upgradeIngredient.quantity <= 0)
            {
                upgradeIngredients.Remove(upgradeIngredient);
            }
        }
    }

    public void DisplayInventory()
    {
        foreach (Transform item in itemContentPanel)
        {
           
            Destroy(item.gameObject);

        }

        foreach (InventoryItem inventoryItem in items)
        {
            GameObject obj = Instantiate(itemPrefab, itemContentPanel);

            var itemName = obj.transform.Find("Title/ItemName").GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.Find("Title/ItemImage").GetComponent<Image>();
            var itemQuantityText = obj.transform.Find("Count/QuantityText").GetComponent<TextMeshProUGUI>();
            var itemDescription = obj.transform.Find("Info/Button/Panel/Description").GetComponent<TextMeshProUGUI>();

            itemName.text = inventoryItem.item.itemName;
            itemImage.sprite = inventoryItem.item.image;
            itemDescription.text = inventoryItem.description;
            itemQuantityText.text = $"x{inventoryItem.quantity}";

           

            obj.GetComponent<ItemUIController>().SetItem(inventoryItem.item);
        }

        foreach (UpgradeIngredient upgradeIngredient in upgradeIngredients)
        {
            GameObject obj = Instantiate(itemPrefab, itemContentPanel);
            var itemName = obj.transform.Find("Title/ItemName").GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.Find("Title/ItemImage").GetComponent<Image>();
            var itemQuantityText = obj.transform.Find("Count/QuantityText").GetComponent<TextMeshProUGUI>();
            var itemDescription = obj.transform.Find("Info/Button/Panel/Description").GetComponent<TextMeshProUGUI>();
            itemName.text = upgradeIngredient.ingredient.ingredientName;
            itemImage.sprite = upgradeIngredient.ingredient.icon;
            itemDescription.text = upgradeIngredient.description;
            itemQuantityText.text = $"x{upgradeIngredient.quantity}";
            // Set the ingredient as the item for the UI controller
            obj.GetComponent<ItemUIController>().SetIngredient(upgradeIngredient.ingredient);
        }
    }

}
