using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum ItemType
//{
//    Hp, Mp, Xp
//}


[CreateAssetMenu(fileName = "item", menuName = "inventory/item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite image;
    //public ItemType itemType;

}
