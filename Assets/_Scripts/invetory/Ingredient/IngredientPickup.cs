using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    public Ingredient ingredient;
    public int count = 1;

    public void PickUp()
    {
        InventoryManager.Instance.AddIngredients(ingredient, count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
