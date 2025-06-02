using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PaneQuest : MonoBehaviour
{
    private bool isShown = false;
    private Vector3 initialPosition;
    private Coroutine coroutine;
    [HideInInspector] public bool isPane;
    //public GameObject Tab;
    public TextMeshProUGUI questItemPrefab;
    public GameObject buttonMuiTen;
    public GameObject textTab;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        
    }

    
    void Update()
    {

        
        // Kiểm tra nếu người chơi nhấn Tab và không có bảng nào đang mở
        if (Input.GetKeyDown(KeyCode.Tab) && !isPane)
        {
            buttonMuiTen.transform.Rotate(0, 0, 180);
            ShowHideQuestsPanel();
            isPane = true;
            textTab.transform.Rotate(0, 0, 180);

        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isPane)
        {
            buttonMuiTen.transform.Rotate(0, 0, 180);
            ShowHideQuestsPanel();
            isPane = false;
            textTab.transform.Rotate(0, 0, 180);
        }


    }
    public void ShowAllQuestItem(List<QuestItem> questItems)
    {
        // xóa danh sách cũ 
        for (int i = 0; i < questItemPrefab.transform.parent.childCount; i++)
        {
            if (questItemPrefab.transform.parent.GetChild(i).gameObject != questItemPrefab.gameObject)
            {
                Destroy(questItemPrefab.transform.parent.GetChild(i).gameObject);
            }
        }


        // Tạo danh sách mới
        foreach (var item in questItems)
        {
            var questItem = Instantiate(questItemPrefab, questItemPrefab.transform.parent);
            questItem.text = $"{item.QuetsItemName} : {item.currentAmount}/{item.questTargetAmount}";
            questItem.gameObject.SetActive(true);
        }
    }


    public void ShowHideQuestsPanel()
    {
        isShown = !isShown;
        
        if (isShown)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(MovingPanel(true));
            
        }
        else
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(MovingPanel(false));
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Tab) && !isPane)
        {
            isPane = true;
            //Tab.SetActive(false);
        }
    }



    IEnumerator MovingPanel(bool show)
    {
        
        while (true)
        {
            var currentX = transform.localPosition.x;
            var currentY = transform.localPosition.y;
            var currentZ = transform.localPosition.z;
            var targetX = show ? initialPosition.x + 50  : initialPosition.x - 210;
            var newX = Mathf.Lerp(currentX, targetX, Time.deltaTime * 2);
            transform.localPosition = new Vector3(newX, currentY, currentZ);

            if (Mathf.Abs(currentX - targetX) < 1)
            {
                break;
            }
            yield return null;

        }

    }



}