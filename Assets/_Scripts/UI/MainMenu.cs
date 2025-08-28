using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasSetting;
    public GameObject mainMenu;

    private void Start()
    {
        Time.timeScale = 1f; 
    }
    public void OnPlayGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("CharacterCreation");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CharacterCreation")
        {
            var upgradeSystem = FindAnyObjectByType<UpgradeSystem>();
            if (upgradeSystem != null)
                upgradeSystem.ResetUpgrades();

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void OnSetting(bool isOpen)
    {
        canvasSetting.SetActive(isOpen);
        mainMenu.SetActive(!isOpen);
    }


    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
