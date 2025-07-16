using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "inventory/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite icon;
    [TextArea] public string description;

    public IngredientType ingredientType; // enum: Material, Weapon, KeyItem, etc.
    public int ingredientsID;        // hoặc dùng GUID nếu muốn

}

public enum IngredientType
{
    Material,
    Weapon,
    Consumable,
    KeyItem
}