using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    public SaveGameManager saveGameManager;
    public Loading loadingScreen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            saveGameManager.curData.positionX = 908;
            saveGameManager.curData.positionY = 2;
            saveGameManager.curData.positionZ = 123;
            saveGameManager.curData.sceneName = "Scene2";

            saveGameManager.SaveWeapons();
            saveGameManager.SaveInventory();
            saveGameManager.SavePlayerStats();

            SaveSystem.SaveGame(saveGameManager.curData);

            loadingScreen.LoadScene("CutScene 2");
        }
    }
}
