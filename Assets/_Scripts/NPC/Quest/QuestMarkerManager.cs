using System.Collections.Generic;
using UnityEngine;

public class QuestMarkerManager : MonoBehaviour
{
    public GameObject markerPrefab;
    private Dictionary<QuestItem, GameObject> activeMarkers = new();

    public void ShowMarker(QuestItem quest)
    {
        if (quest.questLocation == null || activeMarkers.ContainsKey(quest)) return;

        GameObject marker = Instantiate(markerPrefab);
        marker.transform.position = quest.questLocation.position + Vector3.up * 2f;
        marker.GetComponent<QuestFollowTarget>().target = quest.questLocation;
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
