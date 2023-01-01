using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemInteraction : MonoBehaviour
{
    public UserHp userhp;
    public Money money;

    // Margnet
    public GameObject player;
    Vector3 dir;
    float acceleration;
    float velocity;
    UserDataManager SaveLoad;

    void Start()
    {
        userhp = GameObject.FindWithTag("Player").GetComponent<UserHp>();
        money = GameObject.FindWithTag("Money").GetComponent<Money>();

        //Margnet
        player = GameObject.FindWithTag("Player");
        SaveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<UserDataManager>();
        acceleration = 0.0f;
        velocity = 0.0f;
    }
    private void FixedUpdate()
    {
        if (SaveLoad.passiveskill[1] == 1)
            Magnet();
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

    public void Magnet()
    {
        dir = (player.transform.position - transform.position).normalized;
        acceleration = 0.2f;
        velocity = (velocity + acceleration * Time.deltaTime);
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        //효과 범위
        if (distance <= 20.0f)
        {
            this.transform.position = new Vector3(this.transform.position.x + (dir.x * velocity),
                this.transform.position.y,
                this.transform.position.z + (dir.z * velocity));
        }
        else
            velocity = 0.0f;
    }
}
