using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupInfor : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;

    public int countItem;

    public void SetupIngredientInfor(Ingredient ingredient, int count)
    {
        icon.sprite = ingredient.icon;
        itemName.text = ingredient.ingredientName;
        itemAmount.text = "x" + count.ToString();
    }
    public void SetupItemInfor(Item item, int count)
    {
        icon.sprite = item.image;
        itemName.text = item.itemName;
        itemAmount.text = "x" + count.ToString();
    }
}