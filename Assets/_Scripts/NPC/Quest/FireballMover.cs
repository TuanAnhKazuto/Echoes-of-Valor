using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireballMover : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Target & Movement")]
    public Transform questTarget;      // mục tiêu nhiệm vụ
    public float moveSpeed = 15f;      // tốc độ bay
    public float timeMove = 2f;        // thời gian chờ trước khi dừng
    private bool isMoving = false;

    [Header("Hover Settings")]
    public float hoverAmplitude = 0.5f;   
    public float hoverFrequency = 2f;     
    public float upDownSpeed = 0.5f;         


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null) agent = gameObject.AddComponent<NavMeshAgent>();      

    }

    private void Update()
    {
        agent.baseOffset += upDownSpeed * Time.deltaTime;

        if (agent.baseOffset >= 1.5f)
        {
            upDownSpeed = -0.5f;
        }
        else if (agent.baseOffset <= 1f)
        {
            upDownSpeed = 0.5f;
        }

        if (gameObject.transform.position == questTarget.transform.position)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartMove();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StopMove());
        }
    }

    public void StartMove()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null) agent = gameObject.AddComponent<NavMeshAgent>();
        }

        if (questTarget == null) return;

        agent.SetDestination(questTarget.position);
        agent.speed = moveSpeed;
        isMoving = true;
    }

    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(timeMove);
        agent.speed = 0;
        agent.ResetPath();
        isMoving = false;
    }
}
