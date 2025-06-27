using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Loading loadingController;

    public GameObject knightPrefab;
    public GameObject roguePrefab;
    public GameObject magePrefab;
    public GameObject characterHasSpawn;

    private Vector3 spawnPos;
    
    public SaveGameManager saveGameManager;

    private void Awake()
    {
        loadingController.loadingPanel.SetActive(true);
        loadingController.loadingSlider.value = 1f;
        loadingController.percentageText.text = "100%";

        int selectedId = PlayerPrefs.GetInt("SelectedPlayerId");
        Debug.Log($"Selected Player ID: {selectedId}");

        PlayerData curData = SaveSystem.LoadGame(selectedId);

        if (curData.characterClass == "Knight")
        {
            characterHasSpawn = knightPrefab;
        }
        else if (curData.characterClass == "Rogue")
        {
            characterHasSpawn = roguePrefab;
        }
        else if (curData.characterClass == "Mage")
        {
            characterHasSpawn = magePrefab;
        }

        spawnPos = new Vector3(curData.positionX, curData.positionY, curData.positionZ);
    }

    private void Start()
    {
        Instantiate(characterHasSpawn, spawnPos, Quaternion.identity);
        saveGameManager.isCharacterSpawned = true;
        if(saveGameManager.isCharacterSpawned)
        {
            Invoke(nameof(OffPanel), 1f);
        }
    }

    public void OffPanel()
    {
        loadingController.loadingPanel.SetActive(false);
        loadingController.loadingSlider.value = 0f;
        loadingController.percentageText.text = "0%";
    }
}