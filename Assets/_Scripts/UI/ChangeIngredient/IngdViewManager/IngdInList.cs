using UnityEngine;
using static InventoryManager;

public class IngdInList : MonoBehaviour
{
    [SerializeField] private SwitchIngdViewPanelCtrl switchIngdViewPanelCtrl;
    [SerializeField] private SetupIngredientInList setupIngredientInList;
    [SerializeField] private InventoryManager inventoryManager;
    //public Image ingredientImg;

    private void Start()
    {
        setupIngredientInList = GetComponent<SetupIngredientInList>();
        switchIngdViewPanelCtrl = FindAnyObjectByType<SwitchIngdViewPanelCtrl>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public void SelectIngd()
    {
        switchIngdViewPanelCtrl.UpdateIngdExchange(
            setupIngredientInList.ingredientImg,
            setupIngredientInList.ingredientNameString,
            setupIngredientInList.ingredientRank);

        UpdateQuantityAvailable(switchIngdViewPanelCtrl.ingdUse2Change.nameOfIngdWillUse2Change);
    }

    public void UpdateQuantityAvailable(string ingdName)
    {
        foreach (UpgradeIngredient infor in inventoryManager.upgradeIngredients)
        {
            if (infor.ingredient.ingredientName == ingdName)
            {
                switchIngdViewPanelCtrl.UpdateQuantityText(infor.quantity);
                if (infor.ingredient.ingredientName == ingdName)
                {
                    return;
                }
            }
            else 
            {
                switchIngdViewPanelCtrl.UpdateQuantityText(0);
                if(infor.ingredient.ingredientName == null)
                {
                    switchIngdViewPanelCtrl.UpdateQuantityText(0);
                }
            }
        }
    }
}
