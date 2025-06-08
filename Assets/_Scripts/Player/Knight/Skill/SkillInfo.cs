using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    public KnightSkill knightSkill;
    public float damgeSkill = 0;

    private float excaliburDamage = 20f;


    private void Awake()
    {
        if (knightSkill == null)
        {
            knightSkill = GetComponentInParent<KnightSkill>();
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        IsUsingSkill();

        UpdateDamageExcalibur();
    }

    private void IsUsingSkill()
    {
        if (knightSkill.animator.GetCurrentAnimatorStateInfo(0).IsName("1H_Melee_Skill_Excalbur"))
        {
            damgeSkill = excaliburDamage;
        }
        else if (knightSkill.animator.GetCurrentAnimatorStateInfo(0).IsName("1H_Melee_Skill_RageSpike"))
        {
            damgeSkill = 100;
        }
        else
        {
            damgeSkill = 200;
        }
    }

    private void UpdateDamageExcalibur()
    {
        if (knightSkill.excaliburLevel == 1)
        {
            excaliburDamage = 20f;
        }
        else if (knightSkill.excaliburLevel == 2)
        {
            excaliburDamage = 30f;
        }
        else if (knightSkill.excaliburLevel == 3)
        {
            excaliburDamage = 40f;
        }
        else if (knightSkill.excaliburLevel == 4)
        {
            excaliburDamage = 50f;
        }
        else if (knightSkill.excaliburLevel == 5)
        {
            excaliburDamage = 60f;
        }
    }
}
