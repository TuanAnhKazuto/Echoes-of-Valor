using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ItemUIController : MonoBehaviour
{
    public Item item;
    public GameObject infoPanel;             // Panel hiển thị thông tin
    public TextMeshProUGUI infoText;

    private bool isShowingInfo = false;
    //[HideInInspector] public CharacterMovement player;
    //[HideInInspector] public PlayerHealth playerHealth;

    private void Start()
    {
        GameObject pl = GameObject.FindWithTag("Player");
        //player = pl.GetComponent<CharacterMovement>();;
        //playerHealth = pl.GetComponent<PlayerHealth>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
      
        if (infoText != null && item != null)
        {
            infoText.text = item.description; // Gán mô tả cho infoText ngay từ đầu
        }
    }
  
    public void Remove()
    {
        //if(player.curStamina >= player.maxStm || playerHealth.curHp >= playerHealth.maxHp) return;

        InventoryManager.Instance.Remove(item);
        Destroy(this.gameObject);
    }


    public void UseItem()
    {
        switch(item.itemType)
        {
            //case ItemType.Hp:
            //    FindObjectOfType<PlayerHealth>().RecoveryHp(item.value);
            //    break;

            //case ItemType.Mp:
            //    FindObjectOfType<CharacterMovement>().RecoveryMp(item.value);
            //    break;

            case ItemType.Xp:
                FindObjectOfType<EXP>().IncreaseExp(item.value);
                break;
            case ItemType.Hp:
                FindObjectOfType<EXP>().IncreaseExp(-item.value);
                break;

        }
        Remove();
    }
    public void ToggleInfo()
    {
        if (infoPanel == null) return;

        isShowingInfo = !isShowingInfo;
        infoPanel.SetActive(isShowingInfo);

        if (isShowingInfo && item != null && infoText != null)
        {
            infoText.text = item.description;
        }
    }



}
