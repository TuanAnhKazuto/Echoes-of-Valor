using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MenuSetting : MonoBehaviour
{
    [Header("Canvas")]
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

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public List<AudioSource> sfxSources = new List<AudioSource>();

    private GameObject currentCanvas;

    void Start()
    {
        CloseAllCanvas();
        LoadAudioSettings();

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        ApplyVolumes(); // áp dụng ngay khi start
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
        PlayerPrefs.Save();
    }

    void CloseAllCanvas()
    {
        canvasHowToPlay.SetActive(false);
        canvasHelp.SetActive(false);
        canvasThank.SetActive(false);
    }

    void SetMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
        UpdateVolumeText(masterVolumeText, "Master", value);
        ApplyVolumes();
    }

    void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        UpdateVolumeText(musicVolumeText, "Music", value);
        ApplyVolumes();
    }

    void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        UpdateVolumeText(sfxVolumeText, "SFX", value);
        ApplyVolumes();
    }

    void LoadAudioSettings()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        UpdateVolumeText(masterVolumeText, "Master", masterSlider.value);
        UpdateVolumeText(musicVolumeText, "Music", musicSlider.value);
        UpdateVolumeText(sfxVolumeText, "SFX", sfxSlider.value);
    }

    void ApplyVolumes()
    {
        float master = masterSlider.value;
        float music = musicSlider.value;
        float sfx = sfxSlider.value;

        // Music
        if (musicSource != null)
        {
            musicSource.volume = master * music;
            musicSource.mute = (musicSource.volume <= 0.001f);
        }

        // SFX
        foreach (AudioSource sfxSource in sfxSources)
        {
            if (sfxSource != null)
            {
                sfxSource.volume = master * sfx;
                sfxSource.mute = (sfxSource.volume <= 0.001f);
            }
        }
    }

    void UpdateVolumeText(TextMeshProUGUI text, string label, float value)
    {
        int percent = Mathf.RoundToInt(value * 100f);
        text.text = $"{percent}";
    }
}
