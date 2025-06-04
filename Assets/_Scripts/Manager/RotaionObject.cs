using UnityEngine;

public class RotaionObject : MonoBehaviour
{
    public bool horizontalRotation = false;
    public bool verticalRotation = false;

    public float rotationSpeed = 10f;

    private void Update()
    {
        if (horizontalRotation)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }

        if (verticalRotation)
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
