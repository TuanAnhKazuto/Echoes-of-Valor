using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FKey : MonoBehaviour
{
    public GameObject fKey;
    bool isShowFKey;



    private void Start()
    {
        // Nếu chưa được gán sẵn trong Inspector, thì tự động tìm trong scene
        if (fKey == null)
        {
            fKey = GameObject.Find("F key"); // Tên phải đúng chính xác trong Hierarchy
        }

        if (fKey != null)
        {
            fKey.SetActive(false); // Ẩn ngay từ đầu
        }
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
