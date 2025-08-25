using System.Collections.Generic;
using UnityEngine;

public class QuestMarkerManager : MonoBehaviour
{
    public GameObject markerPrefab;
    private Dictionary<QuestItem, GameObject> activeMarkers = new();

    [Header("Fireball dẫn đường")]
    public GameObject fireballPrefab;   // Prefab VFX Fireball
    private GameObject activeFireball;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void ShowMarker(QuestItem quest)
    {
        if (quest.questLocation == null || activeMarkers.ContainsKey(quest)) return;

        Vector3 spawnPos = quest.questLocation.position;

        Collider col = quest.questLocation.GetComponentInChildren<Collider>();
        if (col != null)
        {
            spawnPos.y = col.bounds.max.y;
        }
        else
        {
            spawnPos += Vector3.up * 1.5f;
        }

        GameObject marker = Instantiate(markerPrefab, spawnPos, Quaternion.identity);
        marker.transform.SetParent(quest.questLocation);
        activeMarkers[quest] = marker;

        // 🔥 Spawn Fireball dẫn đường
        if (fireballPrefab != null && player != null)
        {
            if (activeFireball != null) Destroy(activeFireball);

            activeFireball = Instantiate(fireballPrefab, player.position, Quaternion.identity);

            FireballMover mover = activeFireball.AddComponent<FireballMover>();
            mover.InitPath(player, quest.questLocation);
        }
    }

    public void HideMarker(QuestItem quest)
    {
        if (activeMarkers.ContainsKey(quest))
        {
            Destroy(activeMarkers[quest]);
            activeMarkers.Remove(quest);
        }

        if (activeFireball != null)
        {
            Destroy(activeFireball);
            activeFireball = null;
        }
    }

    public void HideAll()
    {
        foreach (var marker in activeMarkers.Values)
        {
            Destroy(marker);
        }
        activeMarkers.Clear();

        if (activeFireball != null)
        {
            Destroy(activeFireball);
            activeFireball = null;
        }
    }
  
}
