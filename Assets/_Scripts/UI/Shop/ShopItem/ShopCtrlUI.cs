using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCtrlUI : MonoBehaviour
{
    public ShopViewPanelCtrl shopView;

    public int buyQuantity;
    public TextMeshProUGUI quantityValueText;

    public int totalPrice;
    public Slider ShopSlider;
    public GameObject notEnoughGoldObj;

    public GameObject noItemSelectedObj;
    public GameObject controlPanelObj;

    private void Start()
    {
        buyQuantity = 1;
        UpdateTotalPrice();
    }

    public void SetupMaxQuantity(int maxQuantity)
    {
        ShopSlider.maxValue = maxQuantity;
    }

    public void UpdateTotalPrice()
    {
        if (shopView.selectedProduct != null)
        {
            totalPrice = shopView.selectedProduct.price * buyQuantity;
            shopView.UpdateTotalPriceText(totalPrice);
        }
    }

    public void OnSliderChanged()
    {
        buyQuantity = (int)ShopSlider.value;
        quantityValueText.text = buyQuantity.ToString();
        UpdateTotalPrice();
    }

    public void IncreaseQuantity()
    {

        buyQuantity++;
        ShopSlider.value = buyQuantity;
        quantityValueText.text = buyQuantity.ToString();
        UpdateTotalPrice();
    }

    public void DecreaseQuantity()
    {
        if (buyQuantity > 1)
        {
            buyQuantity--;
            ShopSlider.value = buyQuantity;
            quantityValueText.text = buyQuantity.ToString();
            UpdateTotalPrice();
        }
    }

    public void BuyButton()
    {
        if (shopView.selectedProduct != null)
        {
            bool success = ShopManager.Instance.BuyItem(shopView.selectedProduct, buyQuantity);

            if (!success)
            {
                
                notEnoughGoldObj.SetActive(true);

              
                CancelInvoke(nameof(HideNotEnoughGold));
                Invoke(nameof(HideNotEnoughGold), 2f);
            }
            else
            {
                
                notEnoughGoldObj.SetActive(false);
            }
        }
    }

    private void HideNotEnoughGold()
    {
        notEnoughGoldObj.SetActive(false);
    }

    public void HideUI()
    {
        controlPanelObj.SetActive(false);
        noItemSelectedObj.SetActive(true);
    }

    public void ShowUI()
    {
        controlPanelObj.SetActive(true);
        noItemSelectedObj.SetActive(false);
    }
    public void ResetQuantity()
    {
        buyQuantity = 1;
        ShopSlider.value = 1;
        quantityValueText.text = "1";
        UpdateTotalPrice();
    }
}
