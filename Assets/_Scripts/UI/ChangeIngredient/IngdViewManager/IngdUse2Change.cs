using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class IngdUse2Change : MonoBehaviour
{
    public Image ingdImg;

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

        foreach (UpgradeIngredient infor in inventoryManager.upgradeIngredients)
        {
            if(infor.ingredient.ingredientName == ingdName)
            {
                if(infor.ingredient.ingredientName == null)
                    switchIngdViewPanelCtrl.UpdateQuantityText(0);
                else
                    switchIngdViewPanelCtrl.UpdateQuantityText(infor.quantity);
            }
            else
            {
                
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
                if(ingredient.ingredientName != ingdName)
                {
                    ingdImg.sprite = ingredient.icon;
                    return;
                }
            }
        }

    }
}