using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInList : MonoBehaviour
{
    public Image itemImg;
    public TextMeshProUGUI itemName;

    public void Setup(Ingredient ingredient)
    {
        if (ingredient == null) return;
        itemImg.sprite = ingredient.icon;
        itemName.text = ingredient.ingredientName;
    }
}