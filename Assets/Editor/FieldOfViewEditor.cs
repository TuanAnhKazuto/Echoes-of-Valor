using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[CustomEditor(typeof(SkeletonMovement))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        SkeletonMovement fov = (SkeletonMovement)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radiusLookAt);

        Vector3 viewAngleA = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.agent / 2);
        Vector3 viewAngleB = DirectionFromAngle(fov.transform.eulerAngles.y, fov.agent / 2);

        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.radiusLookAt);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.radiusLookAt);

        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.ditectionRadius);

        Handles.color = Color.blue;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.attackRange);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}