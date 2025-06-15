using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasSetting;
    public GameObject mainMenu;

    public void OnPlayGame()
    {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene("CharacterCreation");
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
