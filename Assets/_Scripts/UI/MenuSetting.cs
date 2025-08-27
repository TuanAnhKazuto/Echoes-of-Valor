using UnityEngine;

public class MenuSetting : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject canvasControl;
    public GameObject canvasHowToPlay;
    public GameObject canvasThank;
    public GameObject canvasHelp;

    private GameObject currentCanvas;

    void Start()
    {
        CloseAllCanvas();
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
        if (canvasControl != null)
            canvasControl.SetActive(false);
    }
}
