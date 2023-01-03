using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rock : Bullet
{

    Rigidbody rigid;
    NavMeshAgent nav;

    public Transform target;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        nav.SetDestination(target.position);

        Invoke("StopNav", 2f);
        Destroy(gameObject, 4f);
    }
    void StopNav() {
        nav.isStopped = false;
    }
}