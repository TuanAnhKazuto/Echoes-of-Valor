using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIngdCtrlUI : MonoBehaviour
{
    public SwitchIngdViewPanelCtrl switchView;
    public ListIngredient listIngredient;

    public int countChange;
    public TextMeshProUGUI countChangeValueText;

    public int corNeed2Change;

    public int maxIngdCanChange;
    public Slider countSlider;

    public GameObject messengerNullObj;
    public GameObject controlerObj;

    private void Start()
    {
        corNeed2Change = 25;
        countChange = 1;
        CalculateCorNeed();
    }

    public int CalculateCorNeed()
    {
        corNeed2Change = 25 * countChange;
        switchView.UpdateCorText(corNeed2Change);
        return corNeed2Change;
    }
    public void CalculateMaxIngdCanChange(int quantity)
    {
        maxIngdCanChange = quantity / 2;
        countSlider.maxValue = maxIngdCanChange;
    }

    public void ChangeBtn()
    {
        if(switchView.corAvailable.cor < corNeed2Change)
        {
            Debug.Log("Not enough Cor to change ingredients.");
            return;
        }


        Ingredient ingdExchange = listIngredient.ingredientsList.Find
            (i => i.ingredientName == switchView.ingdWillExchanged._name);
        InventoryManager.Instance.AddIngredients(ingdExchange, countChange);

        Ingredient ingdUse2Change = listIngredient.ingredientsList.Find
            (i => i.ingredientName == switchView.ingdUse2Change._name);
        InventoryManager.Instance.RemoveIngredients(ingdUse2Change, countChange * 2);

        switchView.corAvailable.DecreaseCor(corNeed2Change);
        {
            //foreach (InventoryManager.UpgradeIngredient ingd in switchView.inventoryManager.upgradeIngredients)
            //{
            //    if (ingd.ingredient.ingredientName == ingdUse2Change.ingredientName)
            //    {
            //        int quantityAvailable = ingd.quantity;
            //        Debug.Log($"Ingd: {ingd.ingredient.ingredientName} | IngdUse2Change: {ingdUse2Change.ingredientName}; Quantity available: {quantityAvailable}");
            //        switchView.UpdateQuantityIngdText(quantityAvailable);
            //        countChange = 1;
            //        countSlider.value = countChange;
            //        countChangeValueText.text = countChange.ToString();
            //        CalculateCorNeed();
            //        if(ingd.ingredient == null)
            //        {

            //        }
            //        break;
            //    }
            //    else
            //    {
            //        switchView.UpdateQuantityIngdText(0);
            //        Debug.Log($"Ingd: {ingd.ingredient.ingredientName} | IngdUse2Change: {ingdUse2Change.ingredientName}; Quantity available: {0}; ESLE");
            //    }
            //}
        }

        if (switchView.inventoryManager.upgradeIngredients.Find
            (i => i.ingredient.ingredientName == ingdUse2Change.ingredientName) != null)
        {
            int quantityAvailable = switchView.inventoryManager.upgradeIngredients.Find
            (i => i.ingredient.ingredientName == ingdUse2Change.ingredientName).quantity;
            switchView.UpdateQuantityIngdText(quantityAvailable);
            countChange = 1;
            countSlider.value = countChange;
            countChangeValueText.text = countChange.ToString();
            Debug.Log($"IngdUse2Change: {ingdUse2Change.ingredientName}; Quantity available: {quantityAvailable}");

            CalculateCorNeed();
        }
        else
        {
            switchView.UpdateQuantityIngdText(0);
            Debug.Log($"IngdUse2Change: {ingdUse2Change.ingredientName}; Quantity available: {0} ELSE");
        }
    }

    public void ChangeSlideValue()
    {
        countChangeValueText.text = countSlider.value.ToString();
        countChange = (int)countSlider.value;
        CalculateCorNeed();
    }

    public void IncreaseBtn()
    {
        if (countChange >= maxIngdCanChange) return;
        countChange++;
        countSlider.value = countChange;
        switchView.corNeed2ChangeText.text = countChange.ToString();
        CalculateCorNeed();
    }

    public void DecreaseBtn()
    {
        if (countChange > 1)
        {
            countChange--;
            countSlider.value = countChange;
            switchView.corNeed2ChangeText.text = countChange.ToString();
            CalculateCorNeed();
        }
    }

    public void HideChangeUI()
    {
        controlerObj.SetActive(false);
        messengerNullObj.SetActive(true);
    }

    public void ShowChangeUI()
    {
        controlerObj.SetActive(true);
        messengerNullObj.SetActive(false);
    }
}
