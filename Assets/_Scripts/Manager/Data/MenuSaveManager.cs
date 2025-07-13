using System.Collections.Generic;
using UnityEngine;

public class MenuSaveManager : MonoBehaviour
{
    public Transform saveSlotParent; // đối tượng chứa VerticalLayoutGroup
    public GameObject saveSlotPrefab;

    void Start()
    {
        List<PlayerData> allSaves = SaveScanner.LoadAllSaves();

        foreach (PlayerData data in allSaves)
        {
            GameObject go = Instantiate(saveSlotPrefab, saveSlotParent);
            SaveSlotUI slot = go.GetComponent<SaveSlotUI>();
            slot.Setup(data);
        }
    }

}
