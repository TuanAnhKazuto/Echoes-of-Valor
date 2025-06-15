using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject knightPrefab;
    public GameObject roguePrefab;
    public GameObject magePrefab;
    public GameObject characterSpawn;
    
    public SaveGameManager saveGameManager;

    private void Awake()
    {
        saveGameManager = FindAnyObjectByType<SaveGameManager>();

        int selectedId = PlayerPrefs.GetInt("SelectedPlayerId");
        Debug.Log($"Selected Player ID: {selectedId}");

        PlayerData curData = SaveSystem.LoadGame(selectedId);

        if (curData.characterClass == "Knight")
        {
            characterSpawn = knightPrefab;
        }
        else if (curData.characterClass == "Rogue")
        {
            characterSpawn = roguePrefab;
        }
        else if (curData.characterClass == "Mage")
        {
            characterSpawn = magePrefab;
        }
    }

    private void Start()
    {
        Instantiate(characterSpawn, gameObject.transform.position, Quaternion.identity);
        saveGameManager.isCharacterSpawned = true;
    }
}