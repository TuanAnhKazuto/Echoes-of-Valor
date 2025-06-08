using UnityEngine;

public class KnightSkill : MonoBehaviour
{
    public Animator animator;
    

    [Header("Excalibur Skill")]
    public int excaliburLevel = 1;

    public float excarliburCooldown = 6f;
    float nextReadyTime = 0f;

    public GameObject excaliburEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
                nextReadyTime = Time.time + excarliburCooldown;
            }
            else
            {
                Debug.Log("❌ Skill đang hồi, còn: " + (nextReadyTime - Time.time).ToString("F1") + "s");
            }
        }
    }


    public void ExcaliburSkillStart()
    {
        excaliburEffect.SetActive(true);
    }
    public void ExcaliburSkillEnd()
    {
        excaliburEffect.SetActive(false);
    }

}
