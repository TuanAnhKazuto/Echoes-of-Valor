using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Co : MonoBehaviour
{
    public int cor;
    public int CorLarge;
    public int CorMedium;
    public int CorSmall;
    

    public TextMeshProUGUI cointext; 

    public void IncreaseCor(int value)
    {
        cor += value;
        cointext.text = "" + cor.ToString();
    }
    public void IncreaseCorLarge(int value)
    {
        CorLarge += value;
        cointext.text = "" + CorLarge.ToString();
    }
    public void IncreaseCorMedium(int value)
    {
        CorMedium += value;
        cointext.text = "" + CorMedium.ToString();
    }
    public void IncreaseCorSmall(int value)
    {
        CorSmall += value;
        cointext.text = "" + CorSmall.ToString();
    }

    private void Start()
    {

        if (cointext == null)
        {
            cointext = GameObject.Find("Co")?.GetComponent<TextMeshProUGUI>();
        }
    }
}
