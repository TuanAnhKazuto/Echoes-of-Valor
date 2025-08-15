using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class SwitchIngdViewPanelCtrl : MonoBehaviour
{
    public IngdUse2Change ingdUse2Change;
    public IngdWillExchanged ingdWillExchanged;

    public int quantityAvailable;
    public TextMeshProUGUI quantityAvailableText;

    public Cor corAvailable;
    public TextMeshProUGUI corNeed2ChangeText;

    public ChoseIngredientChange changeIngdUsed;

    [Header("Setup for start")]
    public Ingredient ingredientStart;
    public SwitchIngdCtrlUI switchCtrl;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        corAvailable = FindAnyObjectByType<Cor>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    private void OnEnable()
    {
        changeIngdUsed.gameObject.SetActive(false);
        
        SetupWhenStart(ingdUse2Change._name);
    }

    private void SetupWhenStart(string ingdName)
    {
        GameObject imgObj = new("ImgStart", typeof(Image));
        Image img = imgObj.GetComponent<Image>();
        img.sprite = ingredientStart.icon;

        UpdateIngdExchange(img, ingredientStart.ingredientName, ingredientStart.ingredientRank);

        if(inventoryManager.upgradeIngredients == null || inventoryManager.upgradeIngredients.Count == 0)
        {
            switchCtrl.HideChangeUI();
            quantityAvailableText.text = $"<color=#FF0000>{0}</color> / 2";
            return;
        }

        foreach (UpgradeIngredient infor in inventoryManager.upgradeIngredients)
        {
            //UpdateQuantityIngdText(infor.quantity);
            if (infor.ingredient.ingredientName == ingdName)
            {
                UpdateQuantityIngdText(infor.quantity);
                if (infor.ingredient.ingredientName == ingdName)
                {
                    return;
                }
            }
            else
            {
                UpdateQuantityIngdText(0);
                if (infor.ingredient.ingredientName == null)
                {
                    UpdateQuantityIngdText(0);
                }
            }
        }
    }

    public void UpdateQuantityIngdText(int quantity)
    {
        quantityAvailable = quantity;
        switchCtrl.CalculateMaxIngdCanChange(quantityAvailable);
        if(quantityAvailable > 0)
        {
            switchCtrl.ShowChangeUI();
            quantityAvailableText.text = $"<color=#FFFFFF>{quantityAvailable}</color> / 2";
        }
        else
        {
            switchCtrl.HideChangeUI();
            quantityAvailableText.text = $"<color=#FF0000>{quantityAvailable}</color> / 2";
        }
    }

    public void UpdateCorText(int numCor)
    {
        if(corAvailable.cor < numCor)
        {
            corNeed2ChangeText.color = Color.red;
        }
        else
        {
            corNeed2ChangeText.color = Color.white;
        }
        corNeed2ChangeText.text = numCor.ToString();
    }

    public void UpdateIngdExchange(Image img,string ingdName, IngredientRank ingdRank)
    {
        ingdWillExchanged.img.sprite = img.sprite;
        ingdWillExchanged._name = ingdName;
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
