using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    public Animator animator;
    Knight knight;


    [Header("Excalibur Skill")]
    public int excaliburLevel = 1;

    public float excarliburCooldown = 6f;
    float nextReadyTime = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        knight = GetComponent<Knight>();
    }

    private void Update()
    {
        ExcaliburSkill();
    }

    private void ExcaliburSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Time.time >= nextReadyTime)
            {
                animator.SetTrigger("ExcaliburSkill");
                knight.player.canMove = false;
                nextReadyTime = Time.time + excarliburCooldown;
                knight.curTargetRange = knight.skillAttackRange;
                knight.FindClosestEnemy();
            }
            else
            {
                Debug.Log("Skill Excalibur đang hồi, còn: " + (nextReadyTime - Time.time).ToString("F1") + "s");
                knight.player.canMove = true;
                knight.curTargetRange = knight.normalAttackRange;
            }
        }
    }
    public void ExcaliburSkillEnd()
    {
        knight.curTargetRange = knight.normalAttackRange;
        knight.player.canMove = true;
    }

}
