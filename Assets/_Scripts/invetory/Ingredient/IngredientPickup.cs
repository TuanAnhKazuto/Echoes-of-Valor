using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    public Ingredient[] ingredient;
    public int count = 1;

    public void PickUp()
    {
        for(int i = 0; i < ingredient.Length; i++)
        {
            InventoryManager.Instance.AddIngredients(ingredient[i], count);
        }
        
    }
}
