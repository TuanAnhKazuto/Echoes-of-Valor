using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupIngredientInList : MonoBehaviour
{
    public Image ingredientImg;
    public string ingredientNameString;
    public TextMeshProUGUI ingredientName;
    public IngredientRank ingredientRank;

    public void Setup(Ingredient ingredient)
    {
        if (ingredient == null) return;
        ingredientImg.sprite = ingredient.icon;
        ingredientNameString = ingredient.ingredientName;
        ingredientName.text = ingredient.ingredientName;
        ingredientRank = ingredient.ingredientRank; 
    }
}