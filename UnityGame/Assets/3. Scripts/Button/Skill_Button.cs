using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.AI;

public class Skill_Button : MonoBehaviour
{
    public Active_Skill active_Skill;

    public Button[] button;
    public Image[] img_Skill;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            img_Skill[i] = button[i].image;
        }
    }
    public void exc_Skill(int num)
    {
        switch(num)
        {
            case 0:
                //active_Skill.Skill_Slash();
                StartCoroutine(CoolTime(2f, num));
                break;
            case 1:
                StartCoroutine(CoolTime(10f, num));
                break;
            case 2:
                StartCoroutine(CoolTime(5f, num));
                break;
            case 3:
                StartCoroutine(CoolTime(10f, num));
                break;
        }
    }
    IEnumerator CoolTime(float cool, int idx)
    {
        Button setbtn = button[idx].GetComponent<Button>();
         while (cool > 1.0f)
        {
            setbtn.interactable = false;

            cool -= Time.deltaTime;
            img_Skill[idx].fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }
        setbtn.interactable = true;
    }
}
