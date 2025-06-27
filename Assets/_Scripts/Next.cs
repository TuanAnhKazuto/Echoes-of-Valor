using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public Loading loadingController;
    public GameObject screenVideo;

    private void Start()
    {
        screenVideo.SetActive(true);
        StartCoroutine(WaitCutSceneEnd());
    }

    IEnumerator WaitCutSceneEnd()
    {
        yield return new WaitForSeconds(79f);
        NextBtn();
    }

    public void NextBtn()
    {
        screenVideo.SetActive(false);
        loadingController.LoadScene("Scene1");
    }
}