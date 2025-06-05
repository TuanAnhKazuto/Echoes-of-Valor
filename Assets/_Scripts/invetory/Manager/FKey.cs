using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FKey : MonoBehaviour
{
    public GameObject fKey;
    bool isShowFKey;

    private void Start()
    {
        fKey.SetActive(false);
    }

    private void Update()
    {
        if (isShowFKey)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                HideFKey();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            ShowFKey();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            HideFKey();
        }
    }

    public void ShowFKey()
    {
        fKey.SetActive(true);
        isShowFKey = true;
    }

    public void HideFKey()
    {
        fKey.SetActive(false);
        isShowFKey = false;
    }

}
