using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{

    public List<QuestItem> questItems = new List<QuestItem>();

    public PaneQuest playerQuestPanel;

    // Nhận nhiệm vụ 

    public void TakeQuest(QuestItem questItem)
    {

        var check = questItems
                    .FirstOrDefault(x => x.QuetsItemName==
                                questItem.QuetsItemName);

        if (check == null) 
        questItems.Add(questItem);

        Debug.Log("Nhận nhiệm vụ: " + questItem.QuetsItemName);
        // hiển thị 
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


}
