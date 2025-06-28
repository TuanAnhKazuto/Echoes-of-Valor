using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public GameObject welcomePanel;
    public Button okButton;
    public Button exitButton;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public Button confirmButton;

    private int step = 0;
    private bool[] stepCompleted = new bool[6];
    private bool nearNPC = false;
    private bool tutorialEnabled = false;

    void Start()
    {
        welcomePanel.SetActive(true);
        tutorialPanel.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        okButton.onClick.AddListener(StartTutorial);
        exitButton.onClick.AddListener(ExitTutorial);

        confirmButton.onClick.AddListener(NextStep);
    }

    void Update()
    {
        if (!tutorialEnabled || step >= 6)
            return;

        switch (step)
        {
            case 0:
                if (!stepCompleted[0] && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                    CompleteStep(0);
                break;

            case 1:
                if (!stepCompleted[1] && Input.GetKeyDown(KeyCode.LeftShift))
                    CompleteStep(1);
                break;

            case 2:
                if (!stepCompleted[2] && Input.GetKeyDown(KeyCode.E))
                    CompleteStep(2);
                break;

            case 3:
                if (!stepCompleted[3] && (Input.GetMouseButtonDown(0)))
                CompleteStep(3);
                break;
            case 4:
                if (!stepCompleted[4] && Input.GetKeyDown(KeyCode.Alpha2))
                    CompleteStep(4);
                break;

            case 5:
                if (!stepCompleted[5] && nearNPC && Input.GetKeyDown(KeyCode.F))
                    CompleteStep(5);
                break;
        }
    }

    void StartTutorial()
    {
        tutorialEnabled = true;
        welcomePanel.SetActive(false);
        ShowInstructionForStep(step);
    }

    void ExitTutorial()
    {
        tutorialEnabled = false;
        welcomePanel.SetActive(false);
        tutorialPanel.SetActive(false);
    }

    void ShowInstructionForStep(int index)
    {
        tutorialText.text = GetInstructionText(index);
        tutorialPanel.SetActive(true);
        confirmButton.gameObject.SetActive(false); // Ẩn nút Done 
    }

    void CompleteStep(int index)
    {
        stepCompleted[index] = true;
        tutorialText.text = GetCompletedText(index);
        tutorialPanel.SetActive(true);
        confirmButton.gameObject.SetActive(true); // Hiện nút Done
    }

    void NextStep()
    {
        step++;
        if (step >= 5)
        {
            tutorialText.text = "YEHhh Bạn đã hoàn thành hướng dẫn!";
            confirmButton.gameObject.SetActive(false);
            tutorialPanel.SetActive(true);

            StartCoroutine(CloseTutorialAfterDelay());
        }
        else
        {
            ShowInstructionForStep(step);
        }
    }
    IEnumerator CloseTutorialAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        tutorialPanel.SetActive(false);
        tutorialEnabled = false;

    }

    string GetInstructionText(int index)
    {
        switch (index)
        {
            case 0: return "Bấm các phím W, A, S, D để di chuyển nhân vật.";
            case 1: return "Bấm Shift trái để lướt.";
            case 2: return "Bấm phím E để đóng/mở kho đồ.";
            case 3: return "Bấm chuột trái để tấn công thường";
            case 4: return "Bấm phím số 2 để sử dụng kỹ năng. Lưu ý dùng kĩ năng sẽ mất mana";
            case 5: return "Tiến đến gần NPC và bấm F để nói chuyện và nhận nhiệm vụ.";
            default: return "";
        }
    }

    string GetCompletedText(int index)
    {
        switch (index)
        {
            case 0: return "Bạn đã hoàn thành hướng dẫn di chuyển bằng phím W/A/S/D. Nhấn Done để tiếp tục.";
            case 1: return "Bạn đã hoàn thành hướng dẫn lướt bằng Left Shift. Nhấn Done để tiếp tục.";
            case 2: return "Bạn hoàn thành hướng dẫn đã mở kho đồ bằng E. Nhấn E để đóng kho đồ tiếp tục.";
            case 3: return "Bạn đã hoàn thành hướng dẫn tấn công thường. Tiếp theo sẽ đến dùng kĩ năng";
            case 4: return "Bạn hoàn thành hướng dẫn đã sử dụng kỹ năng bằng phím 2. Nhấn các phím số khác để xem kĩ năng.";
            case 5: return "Bạn hoàn thành hướng dẫn đã nói chuyện với NPC bằng phím F. Nhấn Done để kết thúc.";
            default: return "";
        }
    }

    public void SetNearNPC(bool value)
    {
        nearNPC = value;
    }
}
