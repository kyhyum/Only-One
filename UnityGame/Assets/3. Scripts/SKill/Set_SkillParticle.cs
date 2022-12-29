using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_SkillParticle : MonoBehaviour
{
    public ParticleSystem FireWave_particleObject;
    public ParticleSystem AirSlash_particleObject;
    //public ParticleSystem Barrier_particleObject;
    public ParticleSystem Rush_particleObject;

    // Start is called before the first frame update

    void SetUnActive_Skill(ParticleSystem ParticleObject)
    {
         ParticleObject.Stop();
    }
    public void SetActive_Skill(int a)
    {
        switch (a)
        {
            case 0:
                FireWave_particleObject.Play();
                break;
            case 1:
                AirSlash_particleObject.Play();
                break;
            case 2:
               /* for (int i = 0; i < Barrier_particleObject.Length; i++)
                {
                    Barrier_particleObject[i].Play();
                }*/
                break;
            case 3:
                Rush_particleObject.Play();
                break;
        }

    }
    void Start()
    {
        SetUnActive_Skill(Rush_particleObject);
        SetUnActive_Skill(FireWave_particleObject);
        SetUnActive_Skill(AirSlash_particleObject);
        //SetUnActive_Skill(Barrier_particleObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
