using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupInfor : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;

    public int countItem;

    public void SetupItemInfor(IngredientPickup pickup)
    {
        icon.sprite = pickup.ingredient[countItem].icon;
        itemName.text = pickup.ingredient[countItem].name;
        itemAmount.text = pickup.count.ToString();
    }
}
