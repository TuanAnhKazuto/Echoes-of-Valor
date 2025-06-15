using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public CharacterStats player;
    public PlayerData curData;
    public int seletectedId;
    public bool isCharacterSpawned = false;

    public int loadPlayerId;

    private void Start()
    {
        seletectedId = PlayerPrefs.GetInt("SelectedPlayerId");

        curData = SaveSystem.LoadGame(seletectedId);        
    }

    private void Update()
    {
        if (isCharacterSpawned)
        {
            if (!isCharacterSpawned) return;
            Invoke(nameof(Load), 0.5f);
            return;
        }
    }

    private void Load()
    {
        player = FindAnyObjectByType<CharacterStats>();
        LoadPosition();
        isCharacterSpawned = false;
    }

    public void LoadStats()
    {

    }

    private void LoadPosition()
    {
        player.transform.SetPositionAndRotation(new Vector3
            (
                curData.positionX,
                curData.positionY,
                curData.positionZ
            ), Quaternion.Euler(0, curData.rotationY, 0));
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
