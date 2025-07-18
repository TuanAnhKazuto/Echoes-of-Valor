using System.Collections.Generic;
using UnityEngine;

public class QuestMarkerManager : MonoBehaviour
{
    public GameObject markerPrefab; 
    private Dictionary<QuestItem, GameObject> activeMarkers = new();

    public void ShowMarker(QuestItem quest)
    {
        if (quest.questLocation == null || activeMarkers.ContainsKey(quest)) return;

        Vector3 spawnPos = quest.questLocation.position;

        Collider col = quest.questLocation.GetComponentInChildren<Collider>();
        if (col != null)
        {
            spawnPos.y = col.bounds.max.y + 0f;
        }
        else
        {
            spawnPos += Vector3.up * 1.5f;
        }

        GameObject marker = Instantiate(markerPrefab, spawnPos, Quaternion.identity);
        marker.transform.SetParent(quest.questLocation); 

        activeMarkers[quest] = marker;
    }

    public void HideMarker(QuestItem quest)
    {
        if (activeMarkers.ContainsKey(quest))
        {
            Destroy(activeMarkers[quest]);
            activeMarkers.Remove(quest);
        }
    }

    public void HideAll()
    {
        foreach (var marker in activeMarkers.Values)
        {
            Destroy(marker);
        }
        activeMarkers.Clear();
    }
}
