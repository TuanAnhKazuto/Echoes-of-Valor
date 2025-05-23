using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameSceneName = "GameScene";
    public GameObject optionsPanel;

    public void OnNewGame()
    {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene(newGameSceneName);
    }

    public void OnContinue()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.Log("No saved game found!");
        }
    }

    public void OnOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void OnCloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
