using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIngdViewPanelCtrl : MonoBehaviour
{
    public IngdUse2Change ingdUse2Change;
    public IngdWillExchanged ingdWillExchanged;

    public int quantityAvailable;
    public TextMeshProUGUI quantityAvailableText;

    public int countCorNeed2Change;
    public TextMeshProUGUI countCorNeed2ChangeText;

    public ChoseIngredientChange changeIngdUsed;

    private void OnEnable()
    {
        changeIngdUsed.gameObject.SetActive(false);
    }

    public void UpdateQuantityText(int quantity)
    {
        quantityAvailableText.text = quantity.ToString() + " / 2";
    }

    public void UpdateIngdExchange(Image img,string ingdName, IngredientRank ingdRank)
    {
        ingdWillExchanged.ingdWillExchangeImg.sprite = img.sprite;
        ingdUse2Change.UpdateImgInList(ingdRank, ingdName);
        changeIngdUsed.ingdNameString = ingdName;

        if (ingdRank == IngredientRank.Normal)
        {
            changeIngdUsed.ingredientRank = IngredientRank.Normal;
        }
        else if (ingdRank == IngredientRank.Rare)
        {
            changeIngdUsed.ingredientRank = IngredientRank.Rare;
        }
        else if (ingdRank == IngredientRank.Epic)
        {
            changeIngdUsed.ingredientRank = IngredientRank.Epic;
        }
    }

    public void OpenChangeIngUsed()
    {
        changeIngdUsed.gameObject.SetActive(true);
    }
}
