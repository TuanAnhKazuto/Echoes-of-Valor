using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public Loading loadingController;
    public GameObject screenVideo;
    public float timeToWait;
    public string sceneName;

    private void Start()
    {
        screenVideo.SetActive(true);
        StartCoroutine(WaitCutSceneEnd());
    }

    IEnumerator WaitCutSceneEnd()
    {
        yield return new WaitForSeconds(timeToWait);
        NextBtn();
    }

    public void NextBtn()
    {
        screenVideo.SetActive(false);
        loadingController.LoadScene(sceneName);
    }
}