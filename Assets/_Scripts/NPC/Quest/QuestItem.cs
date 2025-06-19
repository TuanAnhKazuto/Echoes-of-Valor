using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    
    public string QuetsItemName; // Tên Quest
    public int questTargetAmount; // số lượng cần tìm
    public int currentAmount ; //số lượng hiện tại 
    public string TargetItemtag; // tag của item cần tìm
    public int rewardAmount = 100;


    // Kiểm tra hoàn thành
    private void Start()
    {
        

    }
    public bool IsComplete()
    {
        return currentAmount >= questTargetAmount;

    }

    // Cập nhật số lượng item
    public void UpdateQuestProgress()
    {
        currentAmount++;
    }

}
