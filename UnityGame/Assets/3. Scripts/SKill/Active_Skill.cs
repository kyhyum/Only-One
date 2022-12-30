using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Skill : MonoBehaviour
{
    Animator animator;

    public Set_SkillParticle set_SkillParticle;

    public BoxCollider FireWave_MeleeArea;
    public BoxCollider AirSlash_MeleeArea;

    public GameObject Shield;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        Shield.SetActive(false);
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
        else if (i == 2)
        {
            Shield.SetActive(true);
            player.isShield = true; 
            yield return new WaitForSeconds(2f);
            Shield.SetActive(false);
            player.isShield = false;
        }
        else if(i == 3) 
        {
            player.damage = 2;
            player.maxspeed = 1.15f;
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
