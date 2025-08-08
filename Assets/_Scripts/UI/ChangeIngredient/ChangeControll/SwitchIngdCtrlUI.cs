using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SwitchIngdCtrlUI : MonoBehaviour
{
    public SwitchIngdViewPanelCtrl switchView;

    public int countChange;
    public TextMeshProUGUI countChangeValue;

    public int corNeed2Change;

    public int maxIngdCanChange;
    public Slider countSlider;

    public GameObject messengerNullObj;
    public GameObject controlerObj;

    private void Start()
    {
        corNeed2Change = 25;
        countChange = 1;
        CalculateCorNeed();
    }

    public int CalculateCorNeed()
    {
        corNeed2Change = 25 * countChange;
        switchView.UpdateCorText(corNeed2Change);
        return corNeed2Change;
    }
    public void CalculateMaxIngdCanChange(int quantity)
    {
        maxIngdCanChange = quantity / 2;
        countSlider.maxValue = maxIngdCanChange;
    }

    public void ChangeBtn()
    {

    }

    public void ChangeSlideValue()
    {
        countChangeValue.text = countSlider.value.ToString();
        countChange = (int)countSlider.value;
        CalculateCorNeed();
    }

    public void IncreaseBtn()
    {
        countChange++;
        countSlider.value = countChange;
        switchView.corNeed2ChangeText.text = countChange.ToString();
        CalculateCorNeed();
    }

    public void DecreaseBtn()
    {
        if (countChange > 0)
        {
            countChange--;
            countSlider.value = countChange;
            switchView.corNeed2ChangeText.text = countChange.ToString();
            CalculateCorNeed();
        }
    }

    public void HideChangeUI()
    {
        controlerObj.SetActive(false);
        messengerNullObj.SetActive(true);
    }

    public void ShowChangeUI()
    {
        controlerObj.SetActive(true);
        messengerNullObj.SetActive(false);
    }
}
