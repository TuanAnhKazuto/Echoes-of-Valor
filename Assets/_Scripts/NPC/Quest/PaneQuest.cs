using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaneQuest : MonoBehaviour
{
    private bool isShown = false;
    private Vector2 initialPosition;
    private Coroutine coroutine;
    [HideInInspector] public bool isPane;

    public TextMeshProUGUI questItemPrefab;
    public GameObject buttonMuiTen;
    public GameObject textTab;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        initialPosition = rect.anchoredPosition;
    }

    void Update()
    {
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
        for (int i = 0; i < questItemPrefab.transform.parent.childCount; i++)
        {
            if (questItemPrefab.transform.parent.GetChild(i).gameObject != questItemPrefab.gameObject)
            {
                Destroy(questItemPrefab.transform.parent.GetChild(i).gameObject);
            }
        }

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

        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(MovingPanel(isShown));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Tab) && !isPane)
        {
            isPane = true;
        }
    }

    IEnumerator MovingPanel(bool show)
    {
        Vector2 targetPos = show
            ? new Vector2(initialPosition.x + 300f, initialPosition.y)
            : new Vector2(initialPosition.x - 5f, initialPosition.y);

        while (Vector2.Distance(rect.anchoredPosition, targetPos) > 1f)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, targetPos, Time.deltaTime * 5);
            yield return null;
        }

        rect.anchoredPosition = targetPos;
    }
}
