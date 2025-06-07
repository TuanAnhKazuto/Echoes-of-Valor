using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject npcChatPanel;
    public TextMeshProUGUI chatText;
    public GameObject yes;
    [HideInInspector] public bool isChating;
    Coroutine coroutine;
    public int maxline;

    // đoạn chat
    public string[] chat;
    // nhiệm vụ
    public QuestItem questItem;

    //Player
    public PlayerQuest PlayerQuests;

    private void Awake()
    {
        yes.SetActive(false);
        npcChatPanel.SetActive(true);
        npcChatPanel.SetActive(false);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerQuests = other.gameObject.GetComponent<PlayerQuest>();
            //yes.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && !isChating)
        {
            isChating = true;
            //yes.SetActive(false);
            npcChatPanel.SetActive(true);
            coroutine = StartCoroutine(ReadChat());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            yes.SetActive(false);
            if (PlayerQuests != null)
            {
                //PlayerQuests.TakeQuest(questItem);

            }

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
        yes.SetActive(true);
        isChating = false;

    }
    // Nhận nhiệm vụ và đóng bảng chat
    public void HidePanel()
    {
        npcChatPanel.SetActive(false);

    }


}