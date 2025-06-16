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
        coin.text = "" + cor.ToString();
        CorLarge += value;
        coin.text = "" + CorLarge.ToString();
        CorMedium += value;
        coin.text = "" + CorMedium.ToString();
        CorSmall += value;
        coin.text = "" + CorSmall.ToString();
    }
    private void Start()
    {
        inventorySetupCor = FindAnyObjectByType<InventorySetup>();
        //if (cointext == null)
        //{
        //    cointext = GameObject.Find("Co")?.GetComponent<TextMeshProUGUI>();
        //}

        if (inventorySetupCor != null)
        {
            coin = inventorySetupCor.Cor.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
