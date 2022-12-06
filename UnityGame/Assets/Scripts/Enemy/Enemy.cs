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
    public GameObject player;

    Rigidbody rigid;
    SphereCollider sphereCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponentInChildren<SphereCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        Invoke("ChaseStart", 2);
    }

    void ChaseStart() {
        isChase = true;
        anim.SetBool("isMove", true);
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
                targetRadius = 0.8f;
                targetRange = 0.8f;
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
        

        switch (enemyType) {
            case Type.Slime:
                anim.SetBool("isAttack", true);
                yield return new WaitForSeconds(0.3f);
                // meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                // meleeArea.enabled = false;
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("sword"))
        {
            if (player.GetComponent<Player>().attacked) {
                StartCoroutine(OnDamage());
                player.GetComponent<Player>().attacked = false;
            }
        }
    }

    IEnumerator OnDamage() {
        // 몬스터가 데미지를 받는 계산 부분
        mat.color = Color.red;
        curHealth -= 1;

        anim.SetTrigger("isHit");

        yield return new WaitForSeconds(0.1f);
        Debug.Log("피격1");
        if (curHealth > 0) {
            mat.color = Color.white;
            Debug.Log("피격2");
            Vector3 reactVec = transform.position - player.GetComponent<Transform>().position;
            rigid.AddForce(reactVec.normalized * 6, ForceMode.Impulse);
            

        } else {
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");

            Destroy(gameObject, 2);
        }
    }
   
}
