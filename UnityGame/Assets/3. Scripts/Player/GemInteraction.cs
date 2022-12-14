using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemInteraction : MonoBehaviour
{
    public UserHp userhp;
    public Money money; 
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            if (collision.gameObject.name.Contains("Heart"))
            {
                userhp.hp_up();
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.name.Contains("Diamond"))
            {
                money.Money_plus(30);
                collision.gameObject.SetActive(false);
            }
            else if (collision.gameObject.name.Contains("Hexgon"))
            {
                money.Money_plus(20);
                collision.gameObject.SetActive(false);
            }
            else if(collision.gameObject.name.Contains("Penta"))
            {
                money.Money_plus(10);
                collision.gameObject.SetActive(false);
            }
        }
    }
}
