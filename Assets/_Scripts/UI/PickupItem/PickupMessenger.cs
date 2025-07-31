using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class PickupMessenger : MonoBehaviour
{
    public static PickupMessenger Instance;
    public GameObject messengerPrefab;
    public Transform messageParent;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPickupIngredientMessage(Ingredient ingredient, int count)
    {
        GameObject messengerObj = Instantiate(messengerPrefab, messageParent);
        PickupInfor info = messengerObj.GetComponent<PickupInfor>();
        info.SetupIngredientInfor(ingredient, count);

        Destroy(messengerObj, 2f);
    }
   
    public void ShowPickupMessage(Item item, int count)
    {
        GameObject messengerObj = Instantiate(messengerPrefab, messageParent);
        PickupInfor info = messengerObj.GetComponent<PickupInfor>();
        info.SetupItemInfor(item, count);

        Destroy(messengerObj, 2f);
    }
}