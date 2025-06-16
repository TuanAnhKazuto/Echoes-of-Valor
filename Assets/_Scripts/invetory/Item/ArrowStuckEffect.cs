using UnityEngine;

public class ArrowStuckEffect : MonoBehaviour
{
    [Header("Prefab Effect Arrow ")]
    public GameObject stuckArrowPrefab;

    public void SpawnStuckArrow(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (stuckArrowPrefab != null)
        {
            GameObject stuckArrow = Instantiate(stuckArrowPrefab, position, rotation);
            if (parent != null)
            {
                stuckArrow.transform.SetParent(parent);
            }
        }
    }
}
