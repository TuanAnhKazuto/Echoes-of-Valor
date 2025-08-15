using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FKey : MonoBehaviour
{
    public GameObject fKey;     
    public GameObject choicePanel;
    public ChoicePanelSetup choicePanelSetup; 
    public Button questButton;
    public Button shopButton;

    [SerializeField] private ShopUIController shopUI;
    private bool isShowFKey;
    private NPC currentNPC;



    private void Start() // chat
    {
        if (fKey != null)
            fKey.SetActive(false); 
        else
            Debug.LogWarning("⚠ Không tìm thấy F key trong scene!");
    }

    private void Update()
    {
        if (isShowFKey && Input.GetKeyDown(KeyCode.F))
        {
            HideFKey(); 

            if (currentNPC != null)
            {                
                OpenChoicePanel();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC")) 
        {
            currentNPC = other.GetComponent<NPC>();

            if (currentNPC != null)
            {
                ShowFKey(); 
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

    private void Awake() // chat
    {
        
        

        
        if (fKey == null)
            fKey = GameObject.Find("F key");

      
        if (choicePanel == null)
            choicePanel = GameObject.Find("ChoicePanel");
        choicePanelSetup = choicePanel.GetComponent<ChoicePanelSetup>();

        if (choicePanel != null)
        {
            choicePanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("⚠ Không tìm thấy ChoicePanel trong scene!");
        }

        shopUI = choicePanelSetup.shopUIController.GetComponent<ShopUIController>();
        if (shopUI == null)
            Debug.LogWarning("⚠ Không tìm thấy ShopUIController trong scene!");


        if (questButton == null)
        {
            questButton = choicePanelSetup.questButton;
        }

        
        if (shopButton == null)
        {
            shopButton = choicePanelSetup.shopButton;
        }
    }

    

    void OpenChoicePanel()
    {
        if (choicePanel == null) return;

        choicePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        questButton.onClick.RemoveAllListeners();
        questButton.onClick.AddListener(() =>
        {
            choicePanel.SetActive(false);
            currentNPC.ManualTrigger();
        });

        shopButton.onClick.RemoveAllListeners();
        shopButton.onClick.AddListener(() =>
        {
            choicePanel.SetActive(false);
            if (shopUI != null)
                shopUI.SetShopActive(true);
        });
    }
}
