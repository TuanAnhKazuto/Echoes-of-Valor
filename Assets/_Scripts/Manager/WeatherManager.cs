using System.Collections;
using Tenkoku.Core;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public TenkokuModule tenkokuModule;
    public float weatherChangeInterval = 300f;
    public float transitionDuration = 5f;

    private void Start()
    {
        tenkokuModule.gameObject.SetActive(true);
        SetWeatherClear();
        StartCoroutine(WeatherChangerRamdom());
    }

    public IEnumerator WeatherChangerRamdom()
    {
        while (true)
        {
            yield return new WaitForSeconds(weatherChangeInterval);
            int random = UnityEngine.Random.Range(0, 4);

            if (random <= 1)
            {
                SetWeatherClear();
            }
            else if (random == 2)
            {
                SetWeatherClouds();
            }
            else if (random >= 3)
            {
                SetWeatherRain();
            }

        }
    }

    private IEnumerator SmoothChangeWeather(System.Action<float> setter, float start, float target)
    {
        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionDuration;
            float value = Mathf.Lerp(start, target, t);
            setter(value);
            yield return null;
        }
        setter(target); // Đảm bảo chính xác giá trị cuối
    }


    public void SetWeatherClear()
    {
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudAltoStratusAmt = v, tenkokuModule.weather_cloudAltoStratusAmt, 0.35f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudCirrusAmt = v, tenkokuModule.weather_cloudCirrusAmt, 0.3f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_OvercastAmt = v, tenkokuModule.weather_OvercastAmt, 0.07f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudSpeed = v, tenkokuModule.weather_cloudSpeed, 0.03f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_RainAmt = v, tenkokuModule.weather_RainAmt, 0.0f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_SnowAmt = v, tenkokuModule.weather_SnowAmt, 0.0f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_FogAmt = v, tenkokuModule.weather_FogAmt, 0.0f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_WindAmt = v, tenkokuModule.weather_WindAmt, 0.2f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_humidity = v, tenkokuModule.weather_humidity, 0.0f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_lightning = v, tenkokuModule.weather_lightning, 0.0f));
    }   

    public void SetWeatherClouds()
    {
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudAltoStratusAmt = v, tenkokuModule.weather_cloudAltoStratusAmt, 0.5f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudCirrusAmt = v, tenkokuModule.weather_cloudCirrusAmt, 0.4f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_OvercastAmt = v, tenkokuModule.weather_OvercastAmt, 0.14f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudSpeed = v, tenkokuModule.weather_cloudSpeed, 0.04f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_RainAmt = v, tenkokuModule.weather_RainAmt, 0.0f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_SnowAmt = v, tenkokuModule.weather_SnowAmt, 0.0f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_FogAmt = v, tenkokuModule.weather_FogAmt, 0.0f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_WindAmt = v, tenkokuModule.weather_WindAmt, 0.3f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_humidity = v, tenkokuModule.weather_humidity, 0.2f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_lightning = v, tenkokuModule.weather_lightning, 0.0f));
    }

    public void SetWeatherRain()
    {
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudAltoStratusAmt = v, tenkokuModule.weather_cloudAltoStratusAmt, 0.6f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudCirrusAmt = v, tenkokuModule.weather_cloudCirrusAmt, 0.1f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_OvercastAmt = v, tenkokuModule.weather_OvercastAmt, 0.2f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_cloudSpeed = v, tenkokuModule.weather_cloudSpeed, 0.18f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_RainAmt = v, tenkokuModule.weather_RainAmt, 0.6f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_SnowAmt = v, tenkokuModule.weather_SnowAmt, 0.0f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_FogAmt = v, tenkokuModule.weather_FogAmt, 0.0f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_WindAmt = v, tenkokuModule.weather_WindAmt, 0.6f));
        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_humidity = v, tenkokuModule.weather_humidity, 0.4f));

        StartCoroutine(SmoothChangeWeather(v => tenkokuModule.weather_lightning = v, tenkokuModule.weather_lightning, 0.5f));
    }
}