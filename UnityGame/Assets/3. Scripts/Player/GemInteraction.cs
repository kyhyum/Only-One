using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemInteraction : MonoBehaviour
{
    public UserHp userhp;
    public Money money; 
    void Start()
    {
        userhp = GameObject.FindWithTag("Player").GetComponent<UserHp>();
        money = GameObject.FindWithTag("Money").GetComponent<Money>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.name.Contains("Heart"))
            {
                userhp.hp_up();
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.name.Contains("Diamond"))
            {
                money.Money_plus(30);
                this.gameObject.SetActive(false);
            }
            else if (this.gameObject.name.Contains("Hexgon"))
            {
                money.Money_plus(20);
                this.gameObject.SetActive(false);
            }
            else if(this.gameObject.name.Contains("Penta"))
            {
                money.Money_plus(10);
                this.gameObject.SetActive(false);
            }
        }
    }
}
