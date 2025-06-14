using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    
    public string QuetsItemName; // Tên Quest
    public int questTargetAmount; // số lượng cần tìm
    public int currentAmount ; //số lượng hiện tại 
    public string TargetItemtag; // tag của item cần tìm

    // Kiểm tra hoàn thành
    private void Start()
    {
        TargetItemtag = "Enemy";
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
