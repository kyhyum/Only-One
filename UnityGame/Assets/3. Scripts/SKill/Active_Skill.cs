using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Skill : MonoBehaviour
{
    Animator animator;

    public Set_SkillParticle set_SkillParticle;

    public BoxCollider FireWave_MeleeArea;
    public BoxCollider AirSlash_MeleeArea;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    public IEnumerator Skill(int i)
    {
        if(i == 0)
        {
            set_SkillParticle.SetActive_Skill(i);
            FireWave_MeleeArea.enabled = true;
            yield return new WaitForSeconds(3f);
            FireWave_MeleeArea.enabled = false;
        }
        else if(i == 1)
        {
            animator.SetTrigger("IsAttack");
            set_SkillParticle.SetActive_Skill(i);
            AirSlash_MeleeArea.enabled = true;
            yield return new WaitForSeconds(0.5f);
            AirSlash_MeleeArea.enabled = false;
        }
        else if(i == 3) 
        {
            player.damage = 2;
            player.maxspeed = 1.1f;
            set_SkillParticle.SetActive_Skill(i);
            yield return new WaitForSeconds(8f);
            player.damage = 1;
            player.maxspeed = 1.05f;
        }
        
    }
    void Update()
    {
        
    }
}
