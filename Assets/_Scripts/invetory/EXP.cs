using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EXP : MonoBehaviour
{
    public int exp;
    

    public TextMeshProUGUI expText;
    
    public void IncreaseExp(int value)
    {
        exp += value;
        expText.text = "Exp: " + exp.ToString();
    }
    private void Start()
    {
        if (expText == null)
        {
            expText = GameObject.Find("Textexp")?.GetComponent<TextMeshProUGUI>();
        }
    }
}


