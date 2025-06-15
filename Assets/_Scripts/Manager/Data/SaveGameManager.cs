using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public CharacterStats playerStats;
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
        playerStats = FindAnyObjectByType<CharacterStats>();
        LoadPosition();
        LoadStats();
        isCharacterSpawned = false;
    }

    public void LoadStats()
    {
        if (curData == null) return;
        playerStats.playerId = curData.playerId;
        playerStats.playerName = curData.playerName;
        playerStats.characterClass = curData.characterClass;
        playerStats.level = curData.level;
        playerStats.maxHealth = curData.health; 
        playerStats.baseDefense = (int)curData.defense;
        playerStats.baseDamage = (int)curData.damage;
        // Load other stats if needed
        // e.g., player.exp = curData.exp; etc.
        LoadPosition();
    }

    private void LoadPosition()
    {
        playerStats.transform.SetPositionAndRotation(new Vector3
            (
                curData.positionX,
                curData.positionY,
                curData.positionZ
            ), Quaternion.Euler(0, curData.rotationY, 0));
    }

    public void SaveGame()
    {
        curData.playerId = playerStats.playerId;
        curData.playerName = playerStats.playerName;
        curData.characterClass = playerStats.characterClass;
        curData.level = playerStats.level;

        curData.health = playerStats.currentHealth;
        curData.defense = playerStats.TotalDefense;
        curData.damage = playerStats.TotalDamage;


        curData.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        curData.positionX = playerStats.transform.position.x;
        curData.positionY = playerStats.transform.position.y;
        curData.positionZ = playerStats.transform.position.z;
        curData.rotationY = playerStats.transform.rotation.eulerAngles.y;

        SaveSystem.SaveGame(curData);
    }
}
