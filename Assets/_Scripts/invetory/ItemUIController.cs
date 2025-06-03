using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ItemUIController : MonoBehaviour
{
    public Item item;
   

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
                FindObjectOfType<Hp>().IncreaseHp(item.value);
                break;
            case ItemType.Mp:
                FindObjectOfType<Mp>().IncreaseMp(item.value);
                break;
        }
        Remove();
        InventoryManager.Instance.DisplayInventory(); 
    }
   



}
