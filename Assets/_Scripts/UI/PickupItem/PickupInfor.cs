using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupInfor : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;

    public int countItem;

    public void SetupItemInfor(Ingredient ingredient, int count)
    {
        icon.sprite = ingredient.icon;
        itemName.text = ingredient.ingredientName;
        itemAmount.text = "x" + count.ToString();
    }
}