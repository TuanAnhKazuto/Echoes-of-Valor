using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "inventory/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite icon;
    [TextArea] public string description;

    public IngredientType ingredientTypeFor;
    public IngredientRank ingredientRank;
    public int ingredientsID;


}

public enum IngredientType
{
    Sword,
    Shield,
    Bow,
    Quiver,
    Stick,
    Book
}

public enum IngredientRank
{
    Normal,
    Rare,
    Epic
}

