using UnityEngine;
using UnityEngine.UI;

public class IngdInList : MonoBehaviour
{
    [SerializeField] private SwitchIngdViewPanelCtrl switchIngdViewPanelCtrl;
    [SerializeField] private SetupIngredientInList setupIngredientInList;
    //public Image ingredientImg;

    private void Start()
    {
        setupIngredientInList = GetComponent<SetupIngredientInList>();
        switchIngdViewPanelCtrl = FindAnyObjectByType<SwitchIngdViewPanelCtrl>();
    }

    public void SelectIngd()
    {
        switchIngdViewPanelCtrl.UpdateIngdExchange(
            setupIngredientInList.ingredientImg,
            setupIngredientInList.ingredientNameString,
            setupIngredientInList.ingredientRank);
    }
}
