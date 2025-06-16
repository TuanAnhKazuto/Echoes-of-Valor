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
    public string[] chat;
    // nhiệm vụ
    public QuestItem questItem;

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
            //yesButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && !isChating)
        {
            playerQuests = other.GetComponent<PlayerQuest>();

            if (questGiven && playerQuests.HasCompletedQuest(questItem))
            {
                // Đã hoàn thành, cho trả nhiệm vụ và nhận thưởng
                playerQuests.CompleteQuest(questItem, 100);
                chatText.text = $"Tuyệt vời! Đây là phần thưởng 100 vàng cho bạn.";
                questCompleted = true;
                npcChatPanel.SetActive(true);
                Invoke(nameof(HidePanel), 2f);
            }
            else if (!questGiven)
            {
                // Chưa nhận, cho xem cốt truyện + nhận nhiệm vụ
                isChating = true;
                npcChatPanel.SetActive(true);
                coroutine = StartCoroutine(ReadChat());
            }
            else
            {
                // Đã nhận nhưng chưa xong
                npcChatPanel.SetActive(true);
                chatText.text = $"Bạn chưa hoàn thành nhiệm vụ: {questItem.QuetsItemName}";
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

        foreach (var line in chat)
        {
            chatText.text = "";

            if (Input.GetKeyDown(KeyCode.Q))
            {
                chatText.text = line;
            }
            else
            {
                for (int i = 0; i < line.Length; i++)
                {

                    chatText.text += line[i];
                    yield return new WaitForSeconds(0.1f);

                }
            }

            yield return new WaitForSeconds(0.5f);

        }
        yesButton.gameObject.SetActive(true);
        yesButton = GameObject.FindWithTag("YesBtn").GetComponent<Button>();
        yesButton.onClick.RemoveAllListeners();

        // Nếu người chơi đã hoàn thành nhiệm vụ
        yesButton.onClick.AddListener(() =>
        {
            if (playerQuests.HasCompletedQuest(questItem))
            {
                // Trả nhiệm vụ và nhận thưởng
                playerQuests.CompleteQuest(questItem, 100);
                chatText.text = $"Tốt lắm! Đây là 100 vàng cho phần thưởng.";

                // (Tùy chọn) Giao nhiệm vụ mới - nếu bạn muốn
                // questItem = nhiệm vụ tiếp theo nếu có
            }
            else if (!playerQuests.questItems.Contains(questItem))
            {
                // Giao nhiệm vụ nếu chưa nhận
                playerQuests.TakeQuest(questItem);
                chatText.text = $"Bạn đã nhận nhiệm vụ: {questItem.QuetsItemName}";
                questGiven = true;
            }
            else
            {
                // Nếu nhiệm vụ đang làm nhưng chưa hoàn thành
                chatText.text = $"Bạn vẫn chưa hoàn thành nhiệm vụ: {questItem.QuetsItemName}";
            }

            yesButton.gameObject.SetActive(false);
            Invoke(nameof(HidePanel), 2f); // Ẩn panel sau 2 giây
        });

        isChating = false;


    }
    // Nhận nhiệm vụ và đóng bảng chat
    public void HidePanel()
    {
        npcChatPanel.SetActive(false);
    }


}