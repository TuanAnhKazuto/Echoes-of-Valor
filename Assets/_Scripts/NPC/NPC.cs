using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static NPC;

public class NPC : MonoBehaviour
{
    // panel NPC và tự động gắn
    public GameObject npcChatPanel;
    public TextMeshProUGUI chatText;
    [HideInInspector] public bool isChating;
    Coroutine coroutine;
    public int maxline;
    public Button yesButton;
    public NpcChatSetup panelSetup;
    private bool questGiven = false;

    // khóa di chuyển 
    public PlayerController playerController;

    // phân loại nhiệm vụ theo từng NPC chính phụ và thêm.
    public enum NpcType
    {
        MainQuest,
        SideQuest,
        Merchant,
    }
    public NpcType npcType;
    // dựa chọn phản hồi
    [System.Serializable]
    public class DialogueChoice
    {
        public string choiceText;
        [TextArea(2, 5)]
        public List<string> followUpLines;
    }
    // đoạn chat
    [System.Serializable]
    public class QuestDialogue
    {
        [TextArea(2, 5)]
        public List<string> lines;
    }
    // đoạn thoại lựa chọn
    [Header("Lựa chọn phản hồi")]
    public List<DialogueChoice> dialogueChoices = new();
    // đoạn thoại cốt truyện
    [Header("Đoạn thoại theo từng nhiệm vụ")]
    public List<QuestDialogue> questChats = new List<QuestDialogue>();
    // nhiệm vụ
    public List<QuestItem> questList;  
    private int currentQuestIndex = 0;
    private QuestItem CurrentQuest => questList[currentQuestIndex];

    //Player
    public PlayerQuest playerQuests;

    private void Awake()
    {
        panelSetup = FindAnyObjectByType<NpcChatSetup>();
    }
    private void Start()
    {
        
        npcChatPanel  = panelSetup.ChatPanel;
        chatText = panelSetup.ChatText.GetComponent<TextMeshProUGUI>();
        yesButton = panelSetup.YesBtn.GetComponent<Button>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerQuests = other.gameObject.GetComponent<PlayerQuest>();
            playerController = other.gameObject.GetComponent<PlayerController>();

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Chỉ cập nhật player nếu chưa gán
            if (playerQuests == null)
                playerQuests = other.GetComponent<PlayerQuest>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yesButton.gameObject.SetActive(false);

            if (isChating)
            {
                StopCoroutine(coroutine); // Dừng đoạn chat 
                coroutine = null;
                isChating = false;
            }

            npcChatPanel.SetActive(false);
        }
    }


    IEnumerator ReadChat()
    {
        List<string> currentChat = (questChats != null && currentQuestIndex < questChats.Count && questChats[currentQuestIndex] != null)
    ? questChats[currentQuestIndex].lines
    : new List<string> { $"Bạn có nhiệm vụ: {CurrentQuest.QuetsItemName}" };

        playerController.canMove = false;

        foreach (var line in currentChat)
        {
            chatText.text = "";
                for (int i = 0; i < line.Length; i++)
                {

                    chatText.text += line[i];
                    yield return new WaitForSeconds(0.05f);

                }

            yield return new WaitForSeconds(0.5f);

        }
        // ⬇️ THÊM ĐOẠN NÀY SAU KHI KẾT THÚC CHAT MỞ ĐẦU
        yield return new WaitForSeconds(0.3f);

        if (dialogueChoices.Count > 0)
        {
            ShowDialogueChoices();
            yield break; 
        }
        else
        {
            yesButton.gameObject.SetActive(true);
            yesButton = GameObject.FindWithTag("YesBtn").GetComponent<Button>();
            yesButton.onClick.RemoveAllListeners();

            yesButton.onClick.AddListener(() =>
            {
                if (playerQuests.HasCompletedQuest(CurrentQuest))
                {
                    QuestItem finishedQuest = CurrentQuest;
                    yesButton.gameObject.SetActive(false);
                    StartCoroutine(ShowAfterCompleteDialogue(finishedQuest));
                }
                else if (!playerQuests.questItems.Contains(CurrentQuest))
                {
                    playerQuests.TakeQuest(CurrentQuest);
                    chatText.text = $"Bạn đã nhận nhiệm vụ: {CurrentQuest.QuetsItemName}";
                    questGiven = true;
                    yesButton.gameObject.SetActive(false);
                    Invoke(nameof(HidePanel), 2f);
                }
                else
                {
                    chatText.text = $"Bạn vẫn chưa hoàn thành nhiệm vụ: {CurrentQuest.QuetsItemName}";
                    yesButton.gameObject.SetActive(false);
                    Invoke(nameof(HidePanel), 2f);
                }
            });
        }

        isChating = false;


    }

    IEnumerator CompleteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        playerQuests.CompleteQuest(CurrentQuest);
        currentQuestIndex++;
        questGiven = false;

        if (currentQuestIndex >= questList.Count)
        {
            chatText.text = "Bạn đã hoàn thành tất cả nhiệm vụ rồi. Cảm ơn bạn.";
        }

        yesButton.gameObject.SetActive(false);
        Invoke(nameof(HidePanel), 2f);
    }
    // đọc thoại khi xong
    IEnumerator ShowAfterCompleteDialogue(QuestItem finishedQuest)
    {
        yield return new WaitForSeconds(1f);

        string dialogue = string.IsNullOrEmpty(finishedQuest.completeDialogue)
            ? $"Bạn đã hoàn thành nhiệm vụ và nhận được {finishedQuest.rewardAmount} vàng."
            : finishedQuest.completeDialogue;

        chatText.text = dialogue;

        yield return new WaitForSeconds(2.5f);
        playerQuests.CompleteQuest(finishedQuest);
        currentQuestIndex++;
        questGiven = false;

        if (currentQuestIndex >= questList.Count)
        {
            chatText.text = "Bạn đã hoàn thành tất cả nhiệm vụ rồi. Cảm ơn bạn.";
            yield return new WaitForSeconds(2f);
        }

        HidePanel();
    }
    public void ManualTrigger()
    {
        if (isChating) return;

        if (currentQuestIndex >= questList.Count)
        {
            npcChatPanel.SetActive(true);
            chatText.text = "Bạn đã hoàn thành tất cả nhiệm vụ rồi. Cảm ơn bạn";
            Invoke(nameof(HidePanel), 2f);
            return;
        }

        if (playerQuests.HasCompletedQuest(CurrentQuest))
        {
            npcChatPanel.SetActive(true);
            yesButton.gameObject.SetActive(false);

            QuestItem finishedQuest = CurrentQuest;
            StartCoroutine(ShowAfterCompleteDialogue(finishedQuest));
        }
        else if (!questGiven)
        {
            isChating = true;
            npcChatPanel.SetActive(true);
            playerController.canMove = false;
            coroutine = StartCoroutine(ReadChat());
        }
        else
        {
            npcChatPanel.SetActive(true);
            chatText.text = $"Nhiệm vụ chưa hoàn thành: {CurrentQuest.QuetsItemName}";
            Invoke(nameof(HidePanel), 2f);
        }
    }

    void ShowDialogueChoices()
    {
        for (int i = 0; i < panelSetup.choiceButtons.Count; i++)
        {
            if (i < dialogueChoices.Count)
            {
                panelSetup.choiceButtons[i].gameObject.SetActive(true);
                panelSetup.choiceTexts[i].text = dialogueChoices[i].choiceText;

                int index = i; // tránh lỗi delegate closure
                panelSetup.choiceButtons[i].onClick.RemoveAllListeners();
                panelSetup.choiceButtons[i].onClick.AddListener(() =>
                {
                    HideAllChoiceButtons();
                    StartCoroutine(PlayFollowUpDialogue(dialogueChoices[index].followUpLines));
                });
            }
            else
            {
                panelSetup.choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }
    void HideAllChoiceButtons()
    {
        foreach (var btn in panelSetup.choiceButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }
    IEnumerator PlayFollowUpDialogue(List<string> lines)
    {
        foreach (var line in lines)
        {
            chatText.text = "";
            for (int i = 0; i < line.Length; i++)
            {
                chatText.text += line[i];
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return));
        }

       
        yesButton.gameObject.SetActive(true);
        yesButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() =>
        {
            if (playerQuests.HasCompletedQuest(CurrentQuest))
            {
                QuestItem finishedQuest = CurrentQuest;
                yesButton.gameObject.SetActive(false);
                StartCoroutine(ShowAfterCompleteDialogue(finishedQuest));
            }
            else if (!playerQuests.questItems.Contains(CurrentQuest))
            {
                playerQuests.TakeQuest(CurrentQuest);
                chatText.text = $"Bạn đã nhận nhiệm vụ: {CurrentQuest.QuetsItemName}";
                questGiven = true;
                yesButton.gameObject.SetActive(false);
                Invoke(nameof(HidePanel), 2f);
            }
            else
            {
                chatText.text = $"Bạn vẫn chưa hoàn thành nhiệm vụ: {CurrentQuest.QuetsItemName}";
                yesButton.gameObject.SetActive(false);
                Invoke(nameof(HidePanel), 2f);
            }
        });
    }

    // Nhận nhiệm vụ và đóng bảng chat
    public void HidePanel()
    {
        npcChatPanel.SetActive(false);

        if (playerController != null)
        {
            playerController.canMove = true;
        }
    }
}