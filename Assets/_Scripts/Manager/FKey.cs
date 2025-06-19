using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FKey : MonoBehaviour
{
    public GameObject fKey;
    bool isShowFKey;
    private NPC currentNPC;



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
        if (isShowFKey && Input.GetKeyDown(KeyCode.F))
        {
            HideFKey(); // ẩn nút F khi nhấn

            if (currentNPC != null)
            {
                // Giả lập việc "kích hoạt" đối thoại
                currentNPC.ManualTrigger();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC")) // Duy nhất 1 tag chung cho tất cả NPC
        {
            currentNPC = other.GetComponent<NPC>();

            if (currentNPC != null)
            {
                ShowFKey(); // Hiện nút F
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            HideFKey();
            currentNPC = null;
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
