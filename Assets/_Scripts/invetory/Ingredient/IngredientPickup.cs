using UnityEngine;

public class IngredientPickup : MonoBehaviour
{
    public Ingredient ingredient;

    public void PickUp()
    {
        InventoryManager.Instance.AddIngredients(ingredient);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
