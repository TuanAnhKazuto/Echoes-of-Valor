using UnityEngine;

public class IngdInChoseList : MonoBehaviour
{
    [SerializeField] private IngdUse2Change ingdUse2Change;
    [SerializeField] private SetupIngredientInList setupIngredientInList;
    [SerializeField] private ChoseIngredientChange choseIngredientChange;

    private void Start()
    {
        ingdUse2Change = FindAnyObjectByType<IngdUse2Change>();
        setupIngredientInList = GetComponent<SetupIngredientInList>();

        choseIngredientChange = GetComponentInParent<ChoseIngredientChange>();  
    }

    public void SelectIngd()
    {
        ingdUse2Change.UpdateIngdUse2Change(
            setupIngredientInList.ingredientImg,
            setupIngredientInList.ingredientNameString);
    }
}