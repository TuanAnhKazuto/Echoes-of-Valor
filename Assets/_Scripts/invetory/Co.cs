using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Co : MonoBehaviour
{
    public int cor;
    public int CorLarge;
    public int CorMedium;
    public int CorSmall;


    InventorySetup inventorySetupCor;
    public TextMeshProUGUI coin;

    public void IncreaseCor(int value)
    {
        cor += value;
        CorLarge += value;
        CorMedium += value;
        CorSmall += value;

        // Hiển thị tổng Cor
        if (coin != null)
            coin.text = cor.ToString();
    }
    private void Start()
    {
        inventorySetupCor = FindAnyObjectByType<InventorySetup>();
        if (inventorySetupCor != null)
        {
            coin = inventorySetupCor.Cor.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
