using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Money : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    //public UserDataManager userdatamanager;
    void Start()
    {
       // userdatamanager = GameObject.FindWithTag("SaveLoad").GetComponent<UserDataManager>();
        moneyText = GetComponent<TextMeshProUGUI>();
       // Money_set(userdatamanager.money);
    }

    public void Money_plus(int plus_num)
    {
        moneyText.text = (Int32.Parse(moneyText.text) + plus_num).ToString();
    }

    public void Money_set(int Money_num)
    {
        moneyText.text = Money_num.ToString();
    }

    public void Money_minus(int minus_num)
    {
        moneyText.text = (Int32.Parse(moneyText.text) - minus_num).ToString();
    }

    /*public void Money_save()
    {
        userdatamanager.money = Int32.Parse(moneyText.text);
        userdatamanager.OverwriteData();
    }*/
}
