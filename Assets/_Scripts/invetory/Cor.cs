using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Cor : MonoBehaviour
{
    public int cor;
    public int CorLarge;
    public int CorMedium;
    public int CorSmall;


    InventorySetup inventorySetupCor;
    public TextMeshProUGUI corText;

    public void IncreaseCor(int value)
    {
        cor += value;
        CorLarge += value;
        CorMedium += value;
        CorSmall += value;

        // Hiển thị tổng Cor
        if (corText != null)
            corText.text = cor.ToString();
    }

    public void DecreaseCor(int value)
    {
        if (cor >= value)
        {
            cor -= value;
            CorLarge -= value;
            CorMedium -= value;
            CorSmall -= value;
            // Cập nhật hiển thị tổng Cor
            if (corText != null)
                corText.text = cor.ToString();
        }
        else
        {
            Debug.LogWarning("Not enough Cor to decrease.");
        }
    }
    private void Start()
    {
        inventorySetupCor = FindAnyObjectByType<InventorySetup>();
        if (inventorySetupCor != null)
        {
            corText = inventorySetupCor.Cor.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
