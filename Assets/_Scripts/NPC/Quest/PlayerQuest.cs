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

                // Cập nhật hiển thị
                playerQuestPanel.ShowAllQuestItem(questItems);

                // Kiểm tra hoàn thành
                if (quest.IsComplete())
                {
                    Debug.Log($"Hoàn thành nhiệm vụ: {quest.QuetsItemName}!");
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

            foreach (var item in questItem.rewardItems)
            {
                InventoryManager.Instance.Add(item);
                Debug.Log($"Nhận thêm vật phẩm: {item.itemName}");
            }

            playerQuestPanel.ShowAllQuestItem(questItems);
        }
    }



}
