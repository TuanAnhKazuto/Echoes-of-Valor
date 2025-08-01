using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Loading : MonoBehaviour
{
    public Slider loadingSlider;
    public TMP_Text percentageText;
    public GameObject loadingPanel;

    private bool isLoading = false;

    public void LoadScene(string sceneName)
    {
        isLoading = true;
        loadingPanel.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float[] checkpoints = { 0.27f, 0.48f, 0.69f, 0.94f }; // giá trị dừng 
        int checkpointIndex = 0;

        while (!operation.isDone)
        {
            float rawProgress = Mathf.Clamp01(operation.progress / 0.9f);

            if (checkpointIndex < checkpoints.Length && rawProgress >= checkpoints[checkpointIndex])
            {
                float percent = Mathf.RoundToInt(checkpoints[checkpointIndex] * 100f);
                loadingSlider.value = checkpoints[checkpointIndex];
                percentageText.text = percent + "%";
                checkpointIndex++;

                yield return new WaitForSeconds(0.5f); 
            }
            else if (checkpointIndex >= checkpoints.Length)
            {
                if (rawProgress >= 1f || operation.progress >= 0.9f)
                {
                    loadingSlider.value = 1f;
                    percentageText.text = "100%";
                    yield return new WaitForSeconds(0.5f);
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

}
