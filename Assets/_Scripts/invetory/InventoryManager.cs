using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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

    public Transform itemContentPane;
    public GameObject itemPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // dong lay
        if (itemContentPane == null)
        {
            itemContentPane = GameObject.Find("Content").transform;

        }
    }


    public void Add(Item item)
    {
        InventoryItem existingItem = items.Find(i => i.item.id == item.id);

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

    public void DisplayInventory()
    {
        foreach (Transform item in itemContentPane)
        {
            Destroy(item.gameObject);
        }

        foreach (InventoryItem inventoryItem in items)
        {
            GameObject obj = Instantiate(itemPrefab, itemContentPane);
            
            var itemName = obj.transform.Find("Title/ItemName").GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.Find("Title/ItemImage").GetComponent<Image>();
            var itemQuantityText = obj.transform.Find("Count/QuantityText").GetComponent<TextMeshProUGUI>();
            var itemDescription = obj.transform.Find("Info/Button/Panel/Description").GetComponent<TextMeshProUGUI>();

            itemName.text = inventoryItem.item.itemName;
            itemImage.sprite = inventoryItem.item.image;
            itemDescription.text = inventoryItem.description;
            itemQuantityText.text = $"x{inventoryItem.quantity}";

            obj.GetComponent<ItemUIController>().SetItem(inventoryItem.item);


            Debug.Log("add item done");
        }
    }

}
