using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public TMP_InputField nameInput;
    public string _characterClass;

    public Button[] characterButtons;
    public Button selectButton;
    public TextMeshProUGUI messageText;

    private int selectedCharacterIndex = -1;

    private void Start()
    {
        nameInput.characterLimit = 16;

        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }



        selectButton.onClick.AddListener(OnSelectPressed);
        ShowMessage("Nhập tên và chọn nhân vật.");
    }

    private void OnCharacterSelected(int index)
    {
        selectedCharacterIndex = index;

        if (selectedCharacterIndex == 0)
        {
            _characterClass = "Rogue";
        }
        else if (selectedCharacterIndex == 1)
        {
            _characterClass = "Knight";
        }
        else if (selectedCharacterIndex == 2)
        {
            _characterClass = "Mage";
        }
        ShowMessage($"Đã chọn nhân vật {_characterClass}.");
    }

    private void OnSelectPressed()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            ShowMessage("Tên không được để trống.");
            return;
        }

        if (selectedCharacterIndex == -1)
        {
            ShowMessage("Vui lòng chọn một nhân vật.");
            return;
        }
        CreateNewPlayer(playerName);

        ShowMessage("Chọn nhân vật thành công ");
        //SceneManager.LoadScene("Scene1");
    }

    public PlayerData CreateNewPlayer(string name)
    {
        PlayerData data = new();
        if (_characterClass == "Knight")
        {
            PlayerData _data = new()
            {
                playerId = PlayerIdGenerator.GetNextAvailableId(),
                playerName = name,
                characterClass = _characterClass,
                level = 1,
                health = 100,
                damage = 10,
                defense = 5,
                worldLevel = 1
            };
            SaveSystem.SaveGame(_data);
            data = _data;
        }
        else if (_characterClass == "Rogue")
        {
            PlayerData _data = new()
            {
                playerId = PlayerIdGenerator.GetNextAvailableId(),
                playerName = name,
                characterClass = _characterClass,
                level = 1,
                health = 80,
                damage = 15,
                defense = 3,
                worldLevel = 1
            };
            SaveSystem.SaveGame(_data);
            data = _data;
        }
        else if (_characterClass == "Mage")
        {
            PlayerData _data = new()
            {
                playerId = PlayerIdGenerator.GetNextAvailableId(),
                playerName = name,
                characterClass = _characterClass,
                level = 1,
                health = 70,
                damage = 20,
                defense = 2,
                worldLevel = 1
            };
            SaveSystem.SaveGame(_data);
            data = _data;
        }

        return data;
    }

    private void ShowMessage(string msg)
    {
        if (messageText != null)
            messageText.text = msg;
    }
}
