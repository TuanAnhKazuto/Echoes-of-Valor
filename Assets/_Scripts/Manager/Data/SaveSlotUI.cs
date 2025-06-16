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

    private int _playerId;
    private string _sceneName;

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
    }

    void SelectThisSave()
    {
        PlayerPrefs.SetInt("SelectedPlayerID", _playerId);
        PlayerPrefs.Save();

        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
    }

}
