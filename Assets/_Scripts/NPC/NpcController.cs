using System.Collections;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            if (animator != null)
            {
                animator.SetTrigger("Interact");
            }
        }
    }
}