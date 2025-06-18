using UnityEngine;

public class SwordTrailController : MonoBehaviour
{
    public TrailRenderer trail;

    void Start()
    {
        if (trail != null)
            trail.emitting = false;
    }

    public void StartTrail()
    {
        if (trail == null) return;
        trail.Clear();
        trail.emitting = true;
    }

    public void StopTrail()
    {
        if (trail == null) return;
        trail.emitting = false;
    }
}
