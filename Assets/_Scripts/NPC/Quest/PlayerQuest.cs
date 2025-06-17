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

    private void Start()
    {
        
        if (playerQuestPanel == null)
        {
            playerQuestPanel = FindAnyObjectByType<PaneQuest>();
        }
       
        
    }
    public void TakeQuest(QuestItem questItem)
    {

        var check = questItems
                    .FirstOrDefault(x => x.QuetsItemName==
                                questItem.QuetsItemName);

        if (check == null) 
        questItems.Add(questItem);

     
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
                //Debug.Log($"Tiến trình nhiệm vụ {quest.QuetsItemName}: {quest.currentAmount}/{quest.questTargetAmount}");

                // Cập nhật hiển thị
                playerQuestPanel.ShowAllQuestItem(questItems);

                // Kiểm tra hoàn thành
                if (quest.IsComplete())
                {
                    //Debug.Log($"Hoàn thành nhiệm vụ: {quest.QuetsItemName}!");
                }
            }
        }
    }

    // Kiểm tra nhiệm vụ đã hoàn thành
    public bool HasCompletedQuest(QuestItem questItem)
    {
        return questItems.Contains(questItem) && questItem.IsComplete();
    }

    // Trả nhiệm vụ, xóa khỏi danh sách và nhận vàng
    public void CompleteQuest(QuestItem questItem, int reward)
    {
        if (HasCompletedQuest(questItem))
        {
            questItems.Remove(questItem);
            //Debug.Log($"Đã trả nhiệm vụ: {questItem.QuetsItemName}, nhận {reward} vàng");

            // Cộng vàng
            FindAnyObjectByType<Co>().IncreaseCor(reward);

            // Cập nhật bảng hiển thị nhiệm vụ
            playerQuestPanel.ShowAllQuestItem(questItems);
        }
    }


}
