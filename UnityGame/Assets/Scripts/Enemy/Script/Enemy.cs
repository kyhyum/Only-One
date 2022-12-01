using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { Slime };
    public Type enemyType;
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public bool isChase;
    public bool isAttack;

    Rigidbody rigid;
    SphereCollider sphereCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        Invoke("ChaseStart", 2);
    }

    void ChaseStart() {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    
    void Update() {
        if (nav.enabled) {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }

    void FreezeVelocity() {
        if (isChase) {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate() {
        Targeting();
        FreezeVelocity();
    }

    void Targeting() {
        float targetRadius = 0;
        float targetRange = 0;

        switch (enemyType) {
            case Type.Slime:
                targetRadius = 1.5f;
                targetRange = 3f;
                break;
        }

        RaycastHit[] rayHits
             = Physics.SphereCastAll(transform.position, targetRadius,
              transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack)
            StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        switch (enemyType) {
            case Type.Slime:
                yield return new WaitForSeconds(0.3f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                meleeArea.enabled = false;
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    void OnTriggerEnter(Collider other) {
        // 몬스터가 데미지를 받는 부분
        // StartCoroutine(OnDamage());
    }

    IEnumerator OnDamage() {
        // 몬스터가 데미지를 받는 계산 부분
        yield return null;
    }
}
