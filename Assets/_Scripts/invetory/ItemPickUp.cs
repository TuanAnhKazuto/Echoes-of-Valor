using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    
    void PickUp()
    {
        if (item.itemType == ItemType.Cor)
        {
            
            Cor coSystem = FindAnyObjectByType<Cor>();
            if (coSystem != null)
            {
                coSystem.IncreaseCor(item.value);
            }
        }
        else
        {
            InventoryManager.Instance.Add(item);
        }

        Destroy(this.gameObject); 

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
            PlayerQuest playerQuest = other.GetComponent<PlayerQuest>();
            if (playerQuest != null)
            {
                playerQuest.UpdateQuest(gameObject.tag);
            }
        }
    }
}
