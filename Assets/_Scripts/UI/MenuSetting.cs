using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSetting : MonoBehaviour
{
    public GameObject canvasControl;
    public GameObject canvasHowToPlay;
    public GameObject canvasThank;
    public GameObject canvasHelp;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Volume Text")]
    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    private GameObject currentCanvas;

    void Start()
    {
        CloseAllCanvas();
        LoadAudioSettings();

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void ShowCanvas(GameObject canvas)
    {
        CloseAllCanvas();
        canvas.SetActive(true);
        currentCanvas = canvas;
    }

    public void OnBack()
    {
        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
            currentCanvas = null;
        }
    }

    void CloseAllCanvas()
    {
        canvasHowToPlay.SetActive(false);
        canvasHelp.SetActive(false);
        canvasThank.SetActive(false);
    }

    void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        UpdateVolumeText(masterVolumeText, "Master", value);
    }

    void SetMusicVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        UpdateVolumeText(musicVolumeText, "Music", value);
    }

    void SetSFXVolume(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        UpdateVolumeText(sfxVolumeText, "SFX", value);
    }
    //Save Volume
    void LoadAudioSettings()
    {
        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;

        AudioListener.volume = master;
        if (musicSource != null) musicSource.volume = music;
        if (sfxSource != null) sfxSource.volume = sfx;

        UpdateVolumeText(masterVolumeText, "Master", master);
        UpdateVolumeText(musicVolumeText, "Music", music);
        UpdateVolumeText(sfxVolumeText, "SFX", sfx);
    }

    void UpdateVolumeText(TextMeshProUGUI text, string label, float value)
    {
        int percent = Mathf.RoundToInt(value * 100f);
        text.text = $"{percent}";
    }
}
