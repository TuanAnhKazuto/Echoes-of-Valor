using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    Animator animator;

    [Header("Excarlibur Skill")]
    public GameObject excaliburEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetTrigger("ExcarliburSkill");
        }
    }


    public void ExcarliburSkillStart()
    {
        excaliburEffect.SetActive(true);
    }
    public void ExcarliburSkillEnd()
    {
        excaliburEffect.SetActive(false);
    }

}
