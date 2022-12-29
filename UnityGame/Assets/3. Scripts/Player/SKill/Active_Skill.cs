using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Skill : MonoBehaviour
{
    Animator animator;

    public Set_SkillParticle set_SkillParticle;

    //public BoxCollider FireWave_MeleeArea;
    //public BoxCollider AirSlash_MeleeArea;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator Skill(int i)
    {
        if(i == 0)
        {
            set_SkillParticle.SetActive_Skill(i);
            yield return new WaitForSeconds(7f);
            /*FireWave_MeleeArea.enabled = true;
            yield return new WaitForSeconds(7f);
            FireWave_MeleeArea.enabled = false;*/
        }
        else if(i == 1)
        {
            animator.SetTrigger("IsAttack");
            set_SkillParticle.SetActive_Skill(i);
            yield return new WaitForSeconds(0.5f);
            /*AirSlash_MeleeArea.enabled = true;
            yield return new WaitForSeconds(0.5f);
            AirSlash_MeleeArea.enabled = false;*/
        }
        else 
        {
            set_SkillParticle.SetActive_Skill(i);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    void Update()
    {
        
    }
}
