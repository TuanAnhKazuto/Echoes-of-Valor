using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Mp : MonoBehaviour
{

    public int mp;
    public TextMeshProUGUI mpText;

    public void IncreaseMp(int value)
    {
        mp += value;
        mpText.text = "MP: " + mp.ToString();
    }
    private void Start()
    {

        if (mpText == null)
        {
            mpText = GameObject.Find("Textmp")?.GetComponent<TextMeshProUGUI>();
        }

    }
}


