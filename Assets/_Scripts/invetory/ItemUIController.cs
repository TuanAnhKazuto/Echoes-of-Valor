using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ItemUIController : MonoBehaviour
{
    public Item item;
    public Ingredient ingredient;

    private void Start()
    {
        GameObject pl = GameObject.FindWithTag("Player");
        
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void SetIngredient(Ingredient _ingredient)
    {
        this.ingredient = _ingredient;
    }
    
    public void Remove()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(this.gameObject);
    }

    public void UseItem()
    {
        if(item == null && ingredient != null) return;

        switch(item.itemType)
        {
            case ItemType.Hp:
                FindAnyObjectByType<CharacterStats>().Heal(item.value);
                break;

            case ItemType.Xp:
                FindAnyObjectByType<EXP>().IncreaseExp(item.value);
                break;
           
            case ItemType.Cor:
                FindAnyObjectByType<Cor>().IncreaseCor(item.value);
                break;

            case ItemType.Mp:
                FindAnyObjectByType<CharacterStats>().ConsumeMana( -item.value);
                break;
        }
        Remove();
        InventoryManager.Instance.DisplayInventory(); 
    }
}
