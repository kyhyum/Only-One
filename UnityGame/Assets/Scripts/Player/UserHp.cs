using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserHp : MonoBehaviour
{
    public Image[] Heart;
    public int Hp;
    private int maxHp;
    public Sprite Back, Front;

    void Start()
    {
        maxHp = 5;
        Hp = maxHp;
        for (int i = 0; i < Hp; i++)
            Heart[i].sprite = Front; 
    }

    public void hp_down()
    {
        Hp -= 1;
        Hp = Mathf.Clamp(Hp, 0, maxHp);
        for (int i = 0; i < maxHp; i++)
            Heart[i].sprite = Back;

        for (int i = 0; i < maxHp; i++)
            if (Hp > i)
            {
                Heart[i].sprite = Front;
            }
    }
}

