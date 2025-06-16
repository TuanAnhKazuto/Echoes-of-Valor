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
           
            case ItemType.Cor:
                FindAnyObjectByType<Co>().IncreaseCor(item.value);
                break;
            //case ItemType.CorLarge:
            //    FindAnyObjectByType<Co>().IncreaseCorLarge(item.value);
            //    break;
            //case ItemType.CorMedium:
            //    FindAnyObjectByType<Co>().IncreaseCorMedium(item.value);
            //    break;
            //case ItemType.CorSmall:
            //    FindAnyObjectByType<Co>().IncreaseCorSmall(item.value);
            //    break;

        }
        Remove();
        InventoryManager.Instance.DisplayInventory(); 
    }
   



}
