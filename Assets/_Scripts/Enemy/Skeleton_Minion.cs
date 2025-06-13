// using UnityEngine;
// using UnityEngine.AI;
//
// public class Skeleton_Minion : BaseCharacter
// {
//     private bool isMoving = false;
//
//     protected override void Start()
//     {
//         base.Start();
//         agent = GetComponent<NavMeshAgent>();
//         if (agent == null) agent = gameObject.AddComponent<NavMeshAgent>();
//         agent.speed = moveSpeed;
//
//     }
//
//     protected override void Update()
//     {
//         base.Update();
//         if (IsDead || IsAttacking) return;
//         
//         distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
//
//         if (distanceToTarget <= attackTriggerRange && distanceToTarget > attackRange)
//         {
//             if (!IsSpawm)
//             {
//                 animator.speed = 1;
//                 return;
//             }
//
//             MoveToTarget();
//         }
//         else if (distanceToTarget <= attackRange)
//         {
//             StopMoving();
//
//             if (!IsFacingTarget())
//             {
//                 RotateToFaceTarget();
//                 return;
//             }
//
//             animator.SetTrigger("Attack");
//             IsAttacking = true;
//         }
//         else
//         {
//             StopMoving();
//         }
//     }
//
//     private void MoveToTarget()
//     {
//         if (agent == null) return;
//
//         float heightDifference = Mathf.Abs(transform.position.y - target.transform.position.y);
//         if (heightDifference > 2f)
//         {
//             StopMoving();
//             return;
//         }
//
//         if (!isMoving)
//         {
//             agent.isStopped = false;
//             isMoving = true;
//             animator.SetBool("IsRunning", true);
//         }
//
//         agent.SetDestination(target.transform.position);
//     }
//
//     private void StopMoving()
//     {
//         if (agent == null) return;
//
//         if (isMoving)
//         {
//             agent.isStopped = true;
//             isMoving = false;
//             animator.SetBool("IsRunning", false);
//         }
//     }
// }
