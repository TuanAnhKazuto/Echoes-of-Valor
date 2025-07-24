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

    public void ShowPickupMessage(Ingredient ingredient, int count)
    {
        GameObject messengerObj = Instantiate(messengerPrefab, messageParent);
        PickupInfor info = messengerObj.GetComponent<PickupInfor>();
        info.SetupItemInfor(ingredient, count);

        Destroy(messengerObj, 2f);
    }
}