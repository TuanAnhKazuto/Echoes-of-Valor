using System.Collections.Generic;
using UnityEngine;

public class ChoseIngredientChange : MonoBehaviour
{
    public GameObject ingredientPrefab;
    public Transform contentPanel;

    public string ingdNameString;
    public IngredientRank ingredientRank;

    public List<Ingredient> ingredientsRankNomalList;
    public List<Ingredient> ingredientsRankRareList;
    public List<Ingredient> ingredientsRankEpicList;

    private void Start()
    {
        ingdNameString = "Sword iron";
    }

    public void OnEnable()
    {
        if(contentPanel == null) return;

        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        if (ingredientRank == IngredientRank.Normal)
        {
            LoadList(ingredientsRankNomalList);
        }
        else if (ingredientRank == IngredientRank.Rare)
        {
            LoadList(ingredientsRankRareList);
        }
        else if (ingredientRank == IngredientRank.Epic)
        {
            LoadList(ingredientsRankEpicList);
        }
    }

    private void LoadList(List<Ingredient> _ingredient)
    {
        foreach (Ingredient ingredient in _ingredient)
        {
            GameObject item = Instantiate(ingredientPrefab, contentPanel);
            SetupIngredientInList itemInList = item.GetComponent<SetupIngredientInList>();
            
            if(ingredient.ingredientName == ingdNameString)
            {
                Destroy(item);
            }

            if (itemInList != null)
            {
                itemInList.Setup(ingredient);
            }
        }
    }

    public void CloseChosePanel()
    {
        gameObject.SetActive(false);
    }
}