using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public TextMeshProUGUI idText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI classText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI worldLvText;

    public Button selectButton;
    public Button deleteButton;

    Loading loadingController;

    private int _playerId;
    private string _sceneName;

    private void Start()
    {
        loadingController = FindAnyObjectByType<Loading>();
    }

    public void Setup(PlayerData data)
    {
        _playerId = data.playerId;
        idText.text = "ID: " + data.playerId;
        nameText.text = "Name: " + data.playerName;
        classText.text = "Class: " + data.characterClass;
        levelText.text = "Level: " + data.level;
        worldLvText.text = "World: " + data.worldLevel;
        _sceneName = data.sceneName;

        selectButton.onClick.AddListener(() => SelectThisSave());
        deleteButton.onClick.AddListener(() => DeleteSave());
    }

    void SelectThisSave()
    {
        PlayerPrefs.SetInt("SelectedPlayerId", _playerId);
        PlayerPrefs.Save();
        loadingController.LoadScene(_sceneName);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
    }

    void DeleteSave()
    {
        SaveSystem.DeleteSave(_playerId);
        Destroy(gameObject);
    }
}
