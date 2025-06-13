using UnityEngine;

public class MenuSetting : MonoBehaviour
{
    public GameObject canvasControl;
    public GameObject canvasHowToPlay;
    public GameObject canvasThank;
    public GameObject canvasHelp;

    [Header("Sound Toggles")]
    public GameObject onMusic, offMusic;
    public GameObject onSFX, offSFX;
    public GameObject onMaster, offMaster;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    private GameObject currentCanvas;

    void Start()
    {
        CloseAllCanvas();
        UpdateSoundUI();
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

    public void ToggleMaster(bool on)
    {
        AudioListener.volume = on ? 1f : 0f;
        UpdateSoundUI();
    }

    public void ToggleMusic(bool on)
    {
        if (musicSource != null)
        {
            musicSource.mute = !on;
        }
        UpdateSoundUI();
    }

    public void ToggleSFX(bool on)
    {
        if (sfxSource != null)
        {
            sfxSource.mute = !on;
        }
        UpdateSoundUI();
    }

    void UpdateSoundUI()
    {
        bool musicOn = !musicSource.mute;
        onMusic.SetActive(!musicOn);  
        offMusic.SetActive(musicOn);  

        bool sfxOn = !sfxSource.mute;
        onSFX.SetActive(!sfxOn);
        offSFX.SetActive(sfxOn);

        bool masterOn = AudioListener.volume > 0f;
        onMaster.SetActive(!masterOn);
        offMaster.SetActive(masterOn);
    }
}
