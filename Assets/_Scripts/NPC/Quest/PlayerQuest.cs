using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerQuest : MonoBehaviour
{
    // sử dụng cho nhiều nhiệm vụ
    public List<QuestItem> questItems = new List<QuestItem>();
    public PaneQuest playerQuestPanel;
    // Nhận nhiệm vụ 
    public GameObject winGamePanel;

    // chỉ dẫn nhiệm vụ
    public QuestMarkerManager markerManager;

    private void Start()
    {

        if (playerQuestPanel == null)
        {
            playerQuestPanel = FindAnyObjectByType<PaneQuest>();
        }

        if (markerManager == null)
        {
            markerManager = FindAnyObjectByType<QuestMarkerManager>();
        }

        if (winGamePanel == null)
        {
            winGamePanel = GameObject.Find("Panel Victory"); 
            if (winGamePanel == null)
                Debug.LogWarning("⚠️ Không tìm thấy Panel Victory trong scene!");
        }
      
        if (winGamePanel != null)
        {
            winGamePanel.SetActive(false);
        }
    }
    public void TakeQuest(QuestItem questItem)
    {

        var check = questItems
                    .FirstOrDefault(x => x.QuetsItemName==
                                questItem.QuetsItemName);

        if (check == null) 
        questItems.Add(questItem);

        if (questItem.questLocation != null)
        {
            markerManager.ShowMarker(questItem);
        }

        playerQuestPanel.ShowAllQuestItem(questItems);
    }

    // Cập nhật tiến trình nhiệm vụ
    public void UpdateQuest(string tag)
    {
        
        foreach (var quest in questItems)
        {
            if (quest.TargetItemtag == tag && !quest.IsComplete())
            {
                quest.UpdateQuestProgress();
                Debug.Log($"Tiến trình nhiệm vụ {quest.QuetsItemName}: {quest.currentAmount}/{quest.questTargetAmount}");
               
                playerQuestPanel.ShowAllQuestItem(questItems);
               
                if (quest.IsComplete())
                {
                    Debug.Log($"Hoàn thành nhiệm vụ: {quest.QuetsItemName}!");
                   
                    if (quest.questLocation != null)
                        markerManager.HideMarker(quest);

                    
                    if (quest.questGiverLocation != null)
                    {
                        quest.questLocation = quest.questGiverLocation; 
                        markerManager.ShowMarker(quest);
                    }
                }
            }
        }
    }

    
    public bool HasCompletedQuest(QuestItem questItem)
    {
        return questItems.Contains(questItem) && questItem.IsComplete();
    }


    public void CompleteQuest(QuestItem questItem)
    {
        if (HasCompletedQuest(questItem))
        {
            questItems.Remove(questItem);

            if (questItem.questLocation != null)
            {
                markerManager.HideMarker(questItem);
            }

            Debug.Log($"Đã trả nhiệm vụ: {questItem.QuetsItemName}, nhận {questItem.rewardAmount} vàng");

            FindAnyObjectByType<Cor>().IncreaseCor(questItem.rewardAmount);        
            Item coinItem = Resources.Load<Item>("Items/GoldItem");
            if (coinItem != null)
            {
                Debug.Log($"Đã load được GoldItem, hiển thị x{questItem.rewardAmount}");
                PickupMessenger.Instance.ShowPickupMessage(coinItem, questItem.rewardAmount);
            }
            else
            {
                Debug.LogWarning("Không tìm thấy GoldItem trong Resources/Items!");
            }

            
            foreach (var item in questItem.rewardItems)
            {
                int amount = item.isCurrency ? questItem.rewardAmount : 1;

                InventoryManager.Instance.Add(item);
                PickupMessenger.Instance.ShowPickupMessage(item, amount);
                Debug.Log($"Nhận thêm vật phẩm: {item.itemName} x{amount}");
            }

            
            foreach (var ingr in questItem.rewardIngredients)
            {
                InventoryManager.Instance.AddIngredients(ingr, questItem.rewardIngredientCount);
                PickupMessenger.Instance.ShowPickupIngredientMessage(ingr, questItem.rewardIngredientCount);
                Debug.Log($"Nhận nguyên liệu: {ingr.ingredientName} x{questItem.rewardIngredientCount}");
            }

            playerQuestPanel.ShowAllQuestItem(questItems);           
        }
    }

    public void ShowWinGame()
    {
        if (winGamePanel == null) return;

        StartCoroutine(ShowWinGameWithDelay(1.5f)); // chờ 1.5 giây sau khi NPC panel tắt
    }

    private IEnumerator ShowWinGameWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        winGamePanel.SetActive(true);

        // Khóa điều khiển
        var playerController = FindAnyObjectByType<PlayerController>();
        if (playerController != null)
        {
            playerController.canMove = false;
            playerController.isTalkingWithNPC = true;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

}
