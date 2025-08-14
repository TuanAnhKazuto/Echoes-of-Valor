using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class IngdUse2Change : MonoBehaviour
{
    public Image ingdImg;
    public string _name;

    public ListIngredient listIngredient;
    public SwitchIngdViewPanelCtrl switchIngdViewPanelCtrl;
    public ChoseIngredientChange changeIngdUsed;

    public InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public void UpdateIngdUse2Change(Image img, string ingdName)
    {
        ingdImg.sprite = img.sprite;
        _name = ingdName;

        foreach (UpgradeIngredient infor in inventoryManager.upgradeIngredients)
        {
            if (infor.ingredient.ingredientName == ingdName)
            {
                switchIngdViewPanelCtrl.UpdateQuantityIngdText(infor.quantity);
                changeIngdUsed.CloseChosePanel();
                return;
            }
            else
            {
                switchIngdViewPanelCtrl.UpdateQuantityIngdText(0);
                if (infor.ingredient.ingredientName == null)
                    switchIngdViewPanelCtrl.UpdateQuantityIngdText(0);
            }
        }
        changeIngdUsed.CloseChosePanel();
    }

    public void UpdateImgInList(IngredientRank ingdRank, string ingdName)
    {
        foreach (Ingredient ingredient in listIngredient.ingredientsList)
        {
            if (ingredient.ingredientRank == ingdRank)
            {
                if (ingredient.ingredientName != ingdName)
                {
                    ingdImg.sprite = ingredient.icon;
                    _name = ingredient.ingredientName;
                    return;
                }
            }
        }

    }
}