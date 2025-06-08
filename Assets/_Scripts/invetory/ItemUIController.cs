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
            case ItemType.Hp:
                FindAnyObjectByType<CharacterStats>().Heal(item.value);
                break;

            case ItemType.Xp:
                FindAnyObjectByType<EXP>().IncreaseExp(item.value);
                break;
           
            case ItemType.Mp:
                FindAnyObjectByType<CharacterStats>().Heal(item.value);
                break;
        }
        Remove();
        InventoryManager.Instance.DisplayInventory(); 
    }
   



}
