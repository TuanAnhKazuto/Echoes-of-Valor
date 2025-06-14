using UnityEngine;
using UnityEngine.AI;

public class SkeletonNecromancer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public float radius = 10f;
    public Vector3 originalePosition;
    public float maxDistance = 50f;
    public Animator animator;

    public enum CharacterState
    {
        Normal,
        Attack
    }
    public CharacterState currentState;

    void Update()
    {
        var distanceToOriginal = Vector3.Distance(target.position, transform.position);
        var distance = Vector3.Distance(target.position, transform.position);
        if (distance <= radius && distanceToOriginal <= maxDistance)
        {
            navMeshAgent.SetDestination(target.position);
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
            distance = Vector3.Distance(target.position, transform.position);
            if(distance < 2f)
            {
                ChangeState(CharacterState.Attack);
            }
        }
        if( distance > radius || distanceToOriginal > maxDistance)
        {
            navMeshAgent.SetDestination(originalePosition);
            distance = Vector3.Distance(originalePosition, transform.position);
            if(distance < 1f)
            {
                animator.SetFloat("Speed", 0);
            }
            ChangeState(CharacterState.Normal);
        }
    }

    private void ChangeState(CharacterState newState)
    {
        switch (currentState)
        {
            case CharacterState.Normal: break;
            case CharacterState.Attack: break;
        }
        switch (newState)
        {
            case CharacterState.Normal: break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
        }
        currentState = newState;
    }
}
