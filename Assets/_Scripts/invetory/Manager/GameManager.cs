using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public PlayerData curData;

    private void Start()
    {
        player = Object.FindFirstObjectByType<PlayerController>();

        curData = SaveSystem.LoadGame();

        player.transform.position = new Vector3
            (
                curData.positionX,
                curData.positionY,
                curData.positionZ
            );

        player.transform.rotation = Quaternion.Euler(0, curData.rotationY, 0);
    }

    public void SaveGame()
    {
        curData.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        curData.positionX = player.transform.position.x;
        curData.positionY = player.transform.position.y;
        curData.positionZ = player.transform.position.z;
        curData.rotationY = player.transform.rotation.eulerAngles.y;

        SaveSystem.SaveGame(curData);
    }
}
