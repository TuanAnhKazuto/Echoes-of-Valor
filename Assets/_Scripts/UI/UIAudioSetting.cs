using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAudioSetting : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Texts (optional)")]
    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    [Header("Panel to Control")]
    public GameObject panelMusic;

    [Header("Buttons")]
    public Button exitButton;  
    public Button openButton;   

    void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            masterSlider.value = AudioManager.Instance.GetMasterVolume();
            musicSlider.value = AudioManager.Instance.GetMusicVolume();
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        }

        UpdateVolumeText(masterVolumeText, masterSlider.value);
        UpdateVolumeText(musicVolumeText, musicSlider.value);
        UpdateVolumeText(sfxVolumeText, sfxSlider.value);

        masterSlider.onValueChanged.AddListener(OnMasterChange);
        musicSlider.onValueChanged.AddListener(OnMusicChange);
        sfxSlider.onValueChanged.AddListener(OnSFXChange);

        if (exitButton != null)
            exitButton.onClick.AddListener(ClosePanel);

        if (openButton != null)
            openButton.onClick.AddListener(OpenPanel);
    }

    void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(OnMasterChange);
        musicSlider.onValueChanged.RemoveListener(OnMusicChange);
        sfxSlider.onValueChanged.RemoveListener(OnSFXChange);

        if (exitButton != null)
            exitButton.onClick.RemoveListener(ClosePanel);

        if (openButton != null)
            openButton.onClick.RemoveListener(OpenPanel);
    }

    void OnMasterChange(float v)
    {
        AudioManager.Instance.SetMasterVolume(v);
        UpdateVolumeText(masterVolumeText, v);
    }

    void OnMusicChange(float v)
    {
        AudioManager.Instance.SetMusicVolume(v);
        UpdateVolumeText(musicVolumeText, v);
    }

    void OnSFXChange(float v)
    {
        AudioManager.Instance.SetSFXVolume(v);
        UpdateVolumeText(sfxVolumeText, v);
    }

    void UpdateVolumeText(TextMeshProUGUI text, float value)
    {
        if (text == null) return;
        text.text = Mathf.RoundToInt(value * 100f).ToString();
    }

    void ClosePanel()
    {
        if (panelMusic != null)
            panelMusic.SetActive(false);
    }

    void OpenPanel()
    {
        if (panelMusic != null)
            panelMusic.SetActive(true);
    }
}
