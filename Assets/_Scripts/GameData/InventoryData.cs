using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int cors;

    public List<InventoryManager.InventoryItem> items = new();

    public List<InventoryManager.UpgradeIngredient> ingredients = new();
}
