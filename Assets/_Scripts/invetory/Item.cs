using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Hp, Mp, Xp
}


[CreateAssetMenu(fileName = "item", menuName = "inventory/item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite image;
    public ItemType itemType;
    public string description;

}
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