using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    public GameObject welcomePanel;
    public Button okButton;
    public Button exitButton;
    public Button skipTutorialButton;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public Button confirmButton;

    public Transform targetPoint;
    public float reachDistance = 2f;

    private Transform player;
    private int step = 0;
    private bool[] stepCompleted = new bool[6];
    private bool tutorialEnabled = false;
    private bool isTyping = false;

    private string currentFullText = "";
    private bool skipRequested = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialDone", 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        //Time.timeScale = 0f;
        StartCoroutine(WaitForPlayer());

        welcomePanel.SetActive(true);
        tutorialPanel.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        okButton.onClick.AddListener(StartTutorial);
        exitButton.onClick.AddListener(ExitTutorial);
        skipTutorialButton.onClick.AddListener(SkipAllTutorial);
        confirmButton.onClick.AddListener(NextStep);
    }


    IEnumerator WaitForPlayer()
    {
        while (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null && found.activeInHierarchy)
            {
                player = found.transform;
                break;
            }
            yield return null;
        }
    }

    void Update()
    {
        if (!tutorialEnabled || step >= 6 || player == null)
            return;

        if (isTyping && Input.GetMouseButtonDown(0))
        {
            skipRequested = true;
            return;
        }

        switch (step)
        {
            case 0:
                if (!stepCompleted[0])
                {
                    bool moved = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
                    bool reachedPoint = Vector3.Distance(player.position, targetPoint.position) <= reachDistance;
                    if (moved && reachedPoint)
                        CompleteStep(0);
                }
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
                if (!stepCompleted[3] && Input.GetMouseButtonDown(0))
                    CompleteStep(3);
                break;

            case 4:
                if (!stepCompleted[4] && Input.GetKeyDown(KeyCode.Alpha2))
                    CompleteStep(4);
                break;

            case 5:
                if (!stepCompleted[5] && Input.GetKeyDown(KeyCode.F))
                {
                    Collider[] hitColliders = Physics.OverlapSphere(player.position, 5f);
                    foreach (var hit in hitColliders)
                    {
                        if (hit.CompareTag("NPC"))
                        {
                            CompleteStep(5);
                            break;
                        }
                    }
                }
                break;

        }
    }

    void StartTutorial()
    {
        tutorialEnabled = true;
        welcomePanel.SetActive(false);
        Time.timeScale = 1f;
        ShowInstructionForStep(step);
    }

    void ExitTutorial()
    {
        tutorialEnabled = false;
        welcomePanel.SetActive(false);
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("TutorialDone", 1);
    }
    public void SkipAllTutorial()
    {
        Time.timeScale = 1f;
        tutorialEnabled = false;
        welcomePanel.SetActive(false);
        tutorialPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ShowInstructionForStep(int index)
    {

        if (index < 0 || index >= stepCompleted.Length)
        {
            return;
        }

        tutorialPanel.SetActive(true);
        confirmButton.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(TypeText(GetInstructionText(index), false)); 
    }

    void CompleteStep(int index)
    {
        if (index < 0 || index >= stepCompleted.Length) return;

        stepCompleted[index] = true;
        tutorialPanel.SetActive(true);
        confirmButton.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(TypeText(GetCompletedText(index), true));
    }

    void NextStep()
    {
        if (isTyping) return;

        step++;
        if (step >= 6)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText("YEHhh Bạn đã hoàn thành hướng dẫn cơ bản!", false)); 
            confirmButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt("TutorialDone", 1);
            StartCoroutine(CloseTutorialAfterDelay());
        }
        else
        {
            ShowInstructionForStep(step);
        }
    }

    IEnumerator CloseTutorialAfterDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        tutorialPanel.SetActive(false);
        tutorialEnabled = false;
        Time.timeScale = 1f;
    }

    IEnumerator TypeText(string fullText, bool showConfirmWhenDone)
    {
        isTyping = true;
        skipRequested = false;
        currentFullText = fullText;

        tutorialText.text = "";

        foreach (char c in fullText)
        {
            if (skipRequested)
            {
                tutorialText.text = fullText;
                break;
            }

            tutorialText.text += c;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        confirmButton.gameObject.SetActive(showConfirmWhenDone);
        isTyping = false;
    }


    string GetInstructionText(int index)
    {
        switch (index)
        {
            case 0: return "Bấm các phím W, A, S, D để di chuyển nhân vật tới gốc cây cạnh bậc thang.";
            case 1: return "Bấm Shift trái để lướt.";
            case 2: return "Bấm phím E để đóng/mở kho đồ.";
            case 3: return "Bấm chuột trái để tấn công thường.";
            case 4: return "Bấm phím số 2 để sử dụng kỹ năng. Lưu ý dùng kĩ năng sẽ mất mana.";
            case 5: return "Tiến đến gần NPC và bấm F để nói chuyện và nhận nhiệm vụ.";
            default: return "";
        }
    }

    string GetCompletedText(int index)
    {
        switch (index)
        {
            case 0: return "Bạn đã hoàn thành hướng dẫn di chuyển đến vị trí! Nhấn Hoàn thành để tiếp tục.";
            case 1: return "Bạn đã hoàn thành hướng dẫn lướt. Nhấn Hoàn thành để tiếp tục.";
            case 2: return "Bạn hoàn thành hướng dẫn đã mở kho đồ. Nhấn Hoàn thành để tiếp tục.";
            case 3: return "Bạn đã hoàn thành hướng dẫn tấn công thường. Nhấn Hoàn thành để tiếp tục.";
            case 4: return "Bạn hoàn thành hướng dẫn đã sử dụng kỹ năng. Nhấn Hoàn thành để tiếp tục.";
            case 5: return "Bạn hoàn thành hướng dẫn đã nói chuyện với NPC. Nhấn Hoàn thành để kết thúc.";
            default: return "";
        }
    }
}
