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
            // cộng vào
            Co coSystem = FindAnyObjectByType<Co>();
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

        //Destroy(this.gameObject);
        //InventoryManager.Instance.Add(item);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
