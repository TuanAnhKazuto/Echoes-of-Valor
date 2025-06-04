using TMPro;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public int hp;


    public TextMeshProUGUI hpText;

    public void IncreaseHp(int value)
    {
        hp += value;
        hpText.text = "HP: " + hp.ToString();
    }

}

