using System.Collections.Generic;
using UnityEngine;

public class ListIngredient : MonoBehaviour
{
    public Transform listSlotParent;
    public GameObject ItemInListPrefab;
    public List<Ingredient> ingredientsList;

    void Start()
    {
        foreach(Ingredient ingredient in ingredientsList)
        {
            GameObject go = Instantiate(ItemInListPrefab, listSlotParent);
            ItemInList itemInList = go.GetComponent<ItemInList>();
            itemInList.Setup(ingredient);
        }

    }
}