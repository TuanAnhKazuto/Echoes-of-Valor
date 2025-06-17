using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject npcChatPanel;
    public TextMeshProUGUI chatText;
    [HideInInspector] public bool isChating;
    Coroutine coroutine;
    public int maxline;
    public Button yesButton;
    public NpcChatSetup panelSetup;
    private bool questGiven = false;
    private bool questCompleted = false;


    // đoạn chat
    [System.Serializable]
    public class QuestDialogue
    {
        [TextArea(2, 5)]
        public List<string> lines;
    }
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
        if (other.gameObject.CompareTag("Player"))
        {
            playerQuests = other.gameObject.GetComponent<PlayerQuest>();
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && !isChating)
        {
            playerQuests = other.GetComponent<PlayerQuest>();

            if (currentQuestIndex >= questList.Count)
            {
                npcChatPanel.SetActive(true);
                chatText.text = "Bạn đã hoàn thành tất cả nhiệm vụ rồi. Cảm ơn bạn";
                Invoke(nameof(HidePanel), 2f);
                return;
            }

            if (playerQuests.HasCompletedQuest(CurrentQuest))
            {
                playerQuests.CompleteQuest(CurrentQuest, 100);
                npcChatPanel.SetActive(true);
                chatText.text = $"Tốt lắm! Nhận 100 vàng";
                currentQuestIndex++; 
                questGiven = false; 
                Invoke(nameof(HidePanel), 2f);
            }
            else if (!questGiven)
            {
                isChating = true;
                npcChatPanel.SetActive(true);
                coroutine = StartCoroutine(ReadChat());
            }
            else
            {
                npcChatPanel.SetActive(true);
                chatText.text = $"Nhiệm vụ chưa hoàn thành: {CurrentQuest.QuetsItemName}";
                Invoke(nameof(HidePanel), 2f);
            }
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


        foreach (var line in currentChat)
        {
            chatText.text = "";
                for (int i = 0; i < line.Length; i++)
                {

                    chatText.text += line[i];
                    yield return new WaitForSeconds(0.1f);

                }
            //}

            yield return new WaitForSeconds(0.3f);

        }
        yesButton.gameObject.SetActive(true);
        yesButton = GameObject.FindWithTag("YesBtn").GetComponent<Button>();
        yesButton.onClick.RemoveAllListeners();

        // Nếu người chơi đã hoàn thành nhiệm vụ
        yesButton.onClick.AddListener(() =>
        {
            if (playerQuests.HasCompletedQuest(CurrentQuest))
            {
                playerQuests.CompleteQuest(CurrentQuest, 100);
                chatText.text = $"Tốt lắm! Đây là 100 vàng cho phần thưởng.";

               // giao nhiem vu moi
            }
            else if (!playerQuests.questItems.Contains(CurrentQuest))
            {
                // Giao nhiệm vụ nếu chưa nhận
                playerQuests.TakeQuest(CurrentQuest);
                chatText.text = $"Bạn đã nhận nhiệm vụ: {CurrentQuest.QuetsItemName}";
                questGiven = true;
            }
            else
            {
                // Nếu nhiệm vụ đang làm nhưng chưa hoàn thành
                chatText.text = $"Bạn vẫn chưa hoàn thành nhiệm vụ: {CurrentQuest.QuetsItemName}";
            }

            yesButton.gameObject.SetActive(false);
            Invoke(nameof(HidePanel), 2f); 
        });

        isChating = false;


    }
    // Nhận nhiệm vụ và đóng bảng chat
    public void HidePanel()
    {
        npcChatPanel.SetActive(false);
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
            playerQuests.CompleteQuest(CurrentQuest, 100);
            npcChatPanel.SetActive(true);
            chatText.text = $"Tốt lắm! Nhận 100 vàng";
            currentQuestIndex++;
            questGiven = false;
            Invoke(nameof(HidePanel), 2f);
        }
        else if (!questGiven)
        {
            isChating = true;
            npcChatPanel.SetActive(true);
            coroutine = StartCoroutine(ReadChat());
        }
        else
        {
            npcChatPanel.SetActive(true);
            chatText.text = $"Nhiệm vụ chưa hoàn thành: {CurrentQuest.QuetsItemName}";
            Invoke(nameof(HidePanel), 2f);
        }
    }



}