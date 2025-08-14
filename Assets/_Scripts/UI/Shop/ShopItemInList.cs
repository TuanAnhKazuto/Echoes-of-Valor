using UnityEngine;

public class ShopItemInList : MonoBehaviour
{
    private ShopViewPanelCtrl shopView;
    private SetupItemInList setupItemInList;

    private void Start()
    {
        setupItemInList = GetComponent<SetupItemInList>();
        shopView = FindAnyObjectByType<ShopViewPanelCtrl>();
    }

    public void SelectItem()
    {
        if (setupItemInList != null && setupItemInList.productData != null)
        {
            shopView.SetSelectedProduct(setupItemInList.productData);
        }
        else
        {
            Debug.LogWarning("⚠ ShopItemInList: productData chưa được gán!");
        }
    }
}
