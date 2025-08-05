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
            SetupIngredientInList itemInList = go.GetComponent<SetupIngredientInList>();
            itemInList.Setup(ingredient);
        }

    }
}