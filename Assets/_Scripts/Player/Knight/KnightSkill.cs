using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    public Animator animator;
    private Knight knight;
    private CharacterStats characterStats;

    [Header("Excalibur Skill")]
    public int excaliburLevel = 1;
    public float excarliburCooldown = 6f;
    private float nextReadyTime = 0f;
    public float excaliburManaCost = 35f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        knight = GetComponent<Knight>();
        characterStats = GetComponentInParent<CharacterStats>();

    }

    private void Update()
    {
        ExcaliburSkill();
    }

    private void ExcaliburSkill()
    {
        if (knight.player.isTalkingWithNPC) return;

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Time.time >= nextReadyTime)
            {
                if (characterStats != null && characterStats.ConsumeMana(excaliburManaCost))
                {
                    animator.SetTrigger("ExcaliburSkill");
                    knight.player.canMove = false;
                    nextReadyTime = Time.time + excarliburCooldown;
                    knight.curTargetRange = knight.skillAttackRange;
                    knight.FindClosestEnemy();
                }
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
