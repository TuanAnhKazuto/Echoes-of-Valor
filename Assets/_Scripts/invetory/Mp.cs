using System;
using TMPro;
using UnityEngine;

public class MP : MonoBehaviour
{
        public int mp;
        public TextMeshProUGUI mpText;

        public void IncreaseMp(int value)
        {
            mp += value;
            mpText.text = "MP: " + mp.ToString();
        }
    }




