using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdminPanelCtrl : MonoBehaviour
{
    public Cor cors;
    public PlayerExpManager expManager;
    public WeatherManager weatherManager;

    [Header("Time Control")]
    public Slider timeSlider;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        Invoke(nameof(GetComponnetWhenStart), 0.3f);
    }

    private void GetComponnetWhenStart()
    {
        cors = FindAnyObjectByType<Cor>();
        expManager = FindAnyObjectByType<PlayerExpManager>();
        weatherManager = FindAnyObjectByType<WeatherManager>();
    }

    public void GetCor(int count)
    {
        cors.IncreaseCor(count);
    }

    public void GetExp(int amount)
    {
        expManager.AddExp(amount);
    }

    public void GetLevel(int amount)
    {
        expManager.AddLevel(amount);
    }

    public void WeatherChangeDropdown(int index)
    {
        switch (index)
        {
            case 0:
                weatherManager.SetWeatherClear();
                break;
            case 1:
                weatherManager.SetWeatherRain();
                break;
        }
    }

    private void OnEnable()
    {
        Invoke(nameof(LoadCurTime), 0.5f);
    }

    private void LoadCurTime()
    {
        timeSlider.value = weatherManager.tenkokuModule.currentHour;
        timeText.text = $"{(int)timeSlider.value}";
    }

    public void ChangeTimeSlider()
    {
        weatherManager.tenkokuModule.currentHour = (int)timeSlider.value;
        timeText.text = $"{(int)timeSlider.value}";
    }
}
