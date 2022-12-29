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
        maxHp = (GameObject.Find("SL System").GetComponent<UserDataManager>().passiveskill[3] == 0) ? 5 : 6;
        Hp = maxHp;
        for (int i = 0; i < Hp; i++)
        {
            Heart[i].gameObject.SetActive(true);
            Heart[i].sprite = Front;
        }
    }

    public void hp_down()
    {
        if (Hp > 0)
        {
            Hp -= 1;
            Hp = Mathf.Clamp(Hp, 0, maxHp);
            hp_check();
        }
    }
    public void hp_up()
    {
        if (Hp > 0 && Hp < maxHp)
        {
            Hp += 1;
            Hp = Mathf.Clamp(Hp, 0, maxHp);
            hp_check();
        }
    }
    public void hp_check()
    {
        for (int i = 0; i < maxHp; i++)
            Heart[i].sprite = Back;

        for (int i = 0; i < maxHp; i++)
            if (Hp > i)
            {
                Heart[i].sprite = Front;
            }
    }
}
