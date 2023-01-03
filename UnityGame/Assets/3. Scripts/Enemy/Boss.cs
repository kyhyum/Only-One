using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    public Transform rockPort;
    public bool isLook;

    float walkingTime = 0;
    Vector3 lookVec;
    Vector3 shotVec;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        Invoke("ChaseStart", 2);
        // Invoke("Think", 2);
        StartCoroutine(Think());
    }

    
    void Update()
    {
        if (isDead) {
            StopAllCoroutines();
            return;
        }

        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }

        if (isLook) {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }

    }

    public IEnumerator Think() {
        yield return new WaitForSeconds(3f);

        int randAction = Random.Range(0, 5);
        switch(randAction) {
            case 0:
            case 1:
                // 원거리 공격
                nav.enabled = false;
                StartCoroutine(Shot());
                break;
            case 2:
            case 3:
            case 4:
                // 근겨리 공격
                nav.enabled = true;
                StartCoroutine(MeleeAttack());
                break;
        }
    }

    IEnumerator Shot() {
        isLook = false;
        anim.SetTrigger("doShot");

        yield return new WaitForSeconds(1.5f);
        GameObject instantRock = Instantiate(bullet, rockPort.position, rockPort.rotation);
        Rock rock = instantRock.GetComponent<Rock>();
        rock.target = target;
        Debug.Log("shot");

        yield return new WaitForSeconds(3f);
        StartCoroutine(Think());
    }

    IEnumerator MeleeAttack() {
        yield return null;
        walkingTime = 0f;

        Debug.Log("근접 공격");
        StartCoroutine(Attack());
        while (walkingTime < 1f) {
            walkingTime += 0.01f;
        }

        if (walkingTime >= 1f) {
            StopCoroutine(MeleeAttack());
            StartCoroutine(Think());
        }
        // if (Vector3.Distance(target.position, transform.position) > 4) {
        //     StopCoroutine(MeleeAttack());
        //     StartCoroutine(Think());
        // } else {
        //     // 근접 공격 실시
        //     isChase = true;
        //     StartCoroutine(Attack());
        // }

        // StartCoroutine(Think());
    }
}