using UnityEngine;
using static InventoryManager;

public class IngdInList : MonoBehaviour
{
    [SerializeField] private SwitchIngdViewPanelCtrl switchView;
    [SerializeField] private SetupIngredientInList setupIngredientInList;
    [SerializeField] private InventoryManager inventoryManager;

    private void Start()
    {
        setupIngredientInList = GetComponent<SetupIngredientInList>();
        switchView = FindAnyObjectByType<SwitchIngdViewPanelCtrl>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public void SelectIngd()
    {
        switchView.UpdateIngdExchange(
            setupIngredientInList.ingredientImg,
            setupIngredientInList.ingredientNameString,
            setupIngredientInList.ingredientRank);

        UpdateQuantityAvailable(switchView.ingdUse2Change._name);
    }

    public void UpdateQuantityAvailable(string ingdName)
    {
        if(inventoryManager.upgradeIngredients == null || inventoryManager.upgradeIngredients.Count == 0)
        {
            switchView.switchCtrl.HideChangeUI();
            return;
        }

        foreach (UpgradeIngredient infor in inventoryManager.upgradeIngredients)
        {
            if (infor.ingredient.ingredientName == ingdName)
            {
                if (infor.ingredient.ingredientName == ingdName)
                {
                    switchView.UpdateQuantityIngdText(infor.quantity);

                    return;
                }
            }
            else
            {
                switchView.UpdateQuantityIngdText(0);

                if (infor.ingredient.ingredientName == null)
                {
                    switchView.UpdateQuantityIngdText(0);
                }
            }
        }
    }
}
