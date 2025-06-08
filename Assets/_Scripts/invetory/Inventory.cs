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

    public bool isInventoryOpen = false; // Trạng thái Inventory

    [HideInInspector] public InventoryManager inventoryManager;

    private void Start()
    {
        GameObject ivtr = GameObject.FindWithTag("InventoryManager");
        inventoryManager = ivtr.GetComponent<InventoryManager>();

        if (inventoryUI == null)
        {
            inventoryUI = GameObject.Find("Window Inventory");
        }

        if (freeLookCamera == null)
        {
            freeLookCamera = FindAnyObjectByType<CinemachineCamera>();
        }

    }

    void Update()
    {
        // Nhấn phím E để mở/đóng Inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
            inventoryManager.DisplayInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen; // Đảo trạng thái Inventory
        inventoryUI.SetActive(isInventoryOpen); // Bật/tắt giao diện Inventory

        if (isInventoryOpen)
        {
            freeLookCamera.enabled = false;
        }
        else
        {
            freeLookCamera.enabled = true;

        }

        // Khóa hoặc mở khóa con trỏ chuột
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isInventoryOpen;
    }

    

}
