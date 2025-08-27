using UnityEngine;

public class LoadingAudioControl : MonoBehaviour
{
    private void OnEnable()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.LoadingPanelOpened();
    }

    private void OnDisable()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.LoadingPanelClosed();
    }
}
