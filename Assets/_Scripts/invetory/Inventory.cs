//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public GameObject inventoryUI; // Tham chiếu tới UI Inventory

    [Header("Free Look Camera Settings")]
    public CinemachineCamera freeLookCamera;

    [Header("Camera Settings")]
    public CinemachineCamera playerCam;    


    public bool isInventoryOpen = false; // Trạng thái Inventory

    [HideInInspector] public InventoryManager inventoryManager;
    InventorySetup inventorySetup;

    private void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        inventorySetup = FindAnyObjectByType<InventorySetup>();

        if (inventoryUI == null)
        {
            inventoryUI = inventorySetup.InventoryWindown;
        }

        if (freeLookCamera == null)
        {
            freeLookCamera = GameObject.Find("PlayerFreeLookCam").GetComponent<CinemachineCamera>();
        }
        playerCam = GameObject.Find("PlayerFreeLookCam").GetComponent<CinemachineCamera>();
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
            inventoryManager.DisplayInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);            

        if (isInventoryOpen)
        {
            playerCam.Priority = 0;
        }
        else
        {
            playerCam.Priority = 10;
        }

        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isInventoryOpen;
    }
}
