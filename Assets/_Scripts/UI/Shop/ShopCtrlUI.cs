using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCtrlUI : MonoBehaviour
{
    public ShopViewPanelCtrl shopView; // Sửa lại: liên kết với script ShopViewPanelCtrl

    public int buyQuantity;
    public TextMeshProUGUI quantityValueText;

    public int totalPrice;
    public Slider ShopSlider;

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
        UpdateTotalPrice();
    }

    public void DecreaseQuantity()
    {
        if (buyQuantity > 1)
        {
            buyQuantity--;
            ShopSlider.value = buyQuantity;
            UpdateTotalPrice();
        }
    }

    public void BuyButton()
    {
        if (shopView.selectedProduct != null)
        {
            ShopManager.Instance.BuyItem(shopView.selectedProduct, buyQuantity);
        }
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
}
