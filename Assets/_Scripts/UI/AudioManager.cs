using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource; // Nhạc menu
    public List<AudioSource> sfxSources = new List<AudioSource>();

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private bool isLoadingPanelActive = false; // panel loading có mở không
    private HashSet<string> muteScenes = new HashSet<string> { "CharacterCreation" };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        // Tắt Play On Awake trong Inspector, nhạc sẽ do script play
        ApplyVolumes();
    }

    // --- Setters ---
    public void SetMasterVolume(float v)
    {
        masterVolume = Mathf.Clamp01(v);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
        ApplyVolumes();
    }

    public void SetMusicVolume(float v)
    {
        musicVolume = Mathf.Clamp01(v);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
        ApplyVolumes();
    }

    public void SetSFXVolume(float v)
    {
        sfxVolume = Mathf.Clamp01(v);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
        ApplyVolumes();
    }

    public void ApplyVolumes()
    {
        if (musicSource != null)
            musicSource.volume = masterVolume * musicVolume;

        foreach (AudioSource sfx in sfxSources)
            if (sfx != null)
                sfx.volume = masterVolume * sfxVolume;

        if (musicSource != null)
            musicSource.mute = isMuteScene() || isLoadingPanelActive || musicSource.volume <= 0.001f;
    }

    public void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    public float GetMasterVolume() => masterVolume;
    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;

    private void OnSceneLoaded(Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        ApplyVolumes();
        TryPlayMusic();
    }

    private bool isMuteScene() => muteScenes.Contains(SceneManager.GetActiveScene().name);

    private void TryPlayMusic()
    {
        if (musicSource == null) return;
        if (!musicSource.isPlaying && !isMuteScene() && !isLoadingPanelActive)
            musicSource.Play();
    }

    // --- Các hàm để UI hoặc panel gọi ---
    public void LoadingPanelOpened()
    {
        isLoadingPanelActive = true;
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Pause();
    }

    public void LoadingPanelClosed()
    {
        isLoadingPanelActive = false;
        ApplyVolumes();
        TryPlayMusic();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
