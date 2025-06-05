using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Button[] characterButtons; 
    public Button selectButton;
    public TextMeshProUGUI messageText; 

    private int selectedCharacterIndex = -1;
    private Dictionary<string, string> playerData = new Dictionary<string, string>();

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
        ShowMessage($"Đã chọn nhân vật {index + 1}.");
    }


    private void OnSelectPressed()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            ShowMessage(" Tên không được để trống.");
            return;
        }

        if (selectedCharacterIndex == -1)
        {
            ShowMessage(" Vui lòng chọn một nhân vật.");
            return;
        }

        string selectedCharacter = "Character" + (selectedCharacterIndex + 1);

        if (playerData.ContainsKey(playerName))
        {
            ShowMessage("Tên này đã được sử dụng.");
            return;
        }

        ShowMessage("Chọn nhân vật thành công ");
        SceneManager.LoadScene("Scene1");
    }

    private void ShowMessage(string msg)
    {
        if (messageText != null)
            messageText.text = msg;
    }
}
