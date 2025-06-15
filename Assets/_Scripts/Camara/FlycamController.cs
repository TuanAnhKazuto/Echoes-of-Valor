using UnityEngine;

public class FlycamController : MonoBehaviour
{
    CharacterController controller;
    public float speed = 10f;
    public float mouseSensitivity = 5f;
    public float xRotation = 0f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
    }
}