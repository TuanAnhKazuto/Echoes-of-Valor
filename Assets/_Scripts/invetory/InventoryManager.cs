using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<Item> items = new List<Item>();

    public Transform itemContentPane;
    public GameObject itemPrefab;

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void Add(Item item)
    {
        items.Add(item);
        DisplayInventory();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void DisplayInventory()
    {
        foreach (Transform item in itemContentPane)
        {
            Destroy(item.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject obj = Instantiate(itemPrefab, itemContentPane);
            var itemName = obj.transform.Find("Title/ItemName").GetComponent<TextMeshProUGUI>();
            var itemImager = obj.transform.Find("ItemImage").GetComponent<Image>();

            itemName.text = item.itemName;
            itemImager.sprite = item.image;


            obj.GetComponent<ItemUIController>().SetItem(item);

            Debug.Log("add item done");

        }
    }


}
