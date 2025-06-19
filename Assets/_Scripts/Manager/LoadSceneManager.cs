using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    public SaveGameManager saveGameManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            saveGameManager.curData.positionX = 908;
            saveGameManager.curData.positionY = 2;
            saveGameManager.curData.positionZ = 123;
            SaveSystem.SaveGame(saveGameManager.curData);

            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2"); 
        }
    }
}
