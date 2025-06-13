using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public CharacterStats player;
    public PlayerData curData;

    public int loadPlayerId;

    private void Start()
    {
        curData = SaveSystem.LoadGame(loadPlayerId);

        if (player == null)
        {
            if (SceneManager.GetActiveScene().name == "CharacterCreation")
                return;
            else
            {
                player = FindAnyObjectByType<CharacterStats>();
                LoadPosition();
            }
        }
    }

    public void LoadStats()
    {

    }

    private void LoadPosition()
    {
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
        curData.playerId = player.playerId;
        curData.playerName = player.playerName;
        curData.characterClass = player.characterClass;
        curData.level = player.level;

        curData.health = player.currentHealth;
        curData.defense = player.TotalDefense;
        curData.damage = player.TotalDamage;


        curData.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        curData.positionX = player.transform.position.x;
        curData.positionY = player.transform.position.y;
        curData.positionZ = player.transform.position.z;
        curData.rotationY = player.transform.rotation.eulerAngles.y;

        SaveSystem.SaveGame(curData);
    }
}
