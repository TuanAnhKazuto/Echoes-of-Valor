using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class UIAudioSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
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


    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            LoadVol();
        }
        else
        {
            ChangeSFXVol();
            ChangeMasterVol();
            ChangeMusicVol();
        }
    }

    public void LoadVol()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol");

        ChangeMasterVol();
        ChangeMusicVol();
        ChangeSFXVol();
    }

    public void ChangeMasterVol()
    {
        float vol = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(vol) * 20);

        float volText = vol * 100;
        masterVolumeText.text = volText.ToString("0");

        PlayerPrefs.SetFloat("MasterVol", vol);
        //PlayerPrefs.Save();
    }

    public void ChangeSFXVol()
    {
        float vol = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(vol) * 20);

        float volText = vol * 100;
        sfxVolumeText.text = volText.ToString("0");

        PlayerPrefs.SetFloat("SFXVol", vol);
    }

    public void ChangeMusicVol()
    {
        float vol = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(vol)*20);

        float volText = vol * 100;
        musicVolumeText.text = volText.ToString("0");

        PlayerPrefs.SetFloat("MusicVol", vol);
    }

    void OnEnable()
    {
        //    if (AudioManager.Instance != null)
        //    {
        //        masterSlider.value = AudioManager.Instance.GetMasterVolume();
        //        musicSlider.value = AudioManager.Instance.GetMusicVolume();
        //        sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        //    }

        //    UpdateVolumeText(masterVolumeText, masterSlider.value);
        //    UpdateVolumeText(musicVolumeText, musicSlider.value);
        //    UpdateVolumeText(sfxVolumeText, sfxSlider.value);

        //    masterSlider.onValueChanged.AddListener(OnMasterChange);
        //    musicSlider.onValueChanged.AddListener(OnMusicChange);
        //    sfxSlider.onValueChanged.AddListener(OnSFXChange);

        if (exitButton != null)
            exitButton.onClick.AddListener(ClosePanel);

        if (openButton != null)
            openButton.onClick.AddListener(OpenPanel);
    }

void OnDisable()
    {
        //masterSlider.onValueChanged.RemoveListener(OnMasterChange);
        //musicSlider.onValueChanged.RemoveListener(OnMusicChange);
        //sfxSlider.onValueChanged.RemoveListener(OnSFXChange);

        if (exitButton != null)
            exitButton.onClick.RemoveListener(ClosePanel);

        if (openButton != null)
            openButton.onClick.RemoveListener(OpenPanel);
    }

    //void OnMasterChange(float v)
    //{
    //    AudioManager.Instance.SetMasterVolume(v);
    //    UpdateVolumeText(masterVolumeText, v);
    //}

    //void OnMusicChange(float v)
    //{
    //    AudioManager.Instance.SetMusicVolume(v);
    //    UpdateVolumeText(musicVolumeText, v);
    //}

    //void OnSFXChange(float v)
    //{
    //    AudioManager.Instance.SetSFXVolume(v);
    //    UpdateVolumeText(sfxVolumeText, v);
    //}

    //void UpdateVolumeText(TextMeshProUGUI text, float value)
    //{
    //    if (text == null) return;
    //    text.text = Mathf.RoundToInt(value * 100f).ToString();
    //}



    public void ClosePanel()
    {
        if (panelMusic != null)
            panelMusic.SetActive(false);
    }

    public void OpenPanel()
    {
        if (panelMusic != null)
            panelMusic.SetActive(true);
    }
}
