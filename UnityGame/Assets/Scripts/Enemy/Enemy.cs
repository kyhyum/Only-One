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
    public bool isChase;
    public bool isAttack;
    public bool isHit;
    public GameObject player;

    Rigidbody rigid;
    SphereCollider sphereCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;
    float asd;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        asd = rigid.velocity.y;
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
                targetRadius = 0.8f;
                targetRange = 1.2f;
                break;
        }

        RaycastHit[] rayHits
             = Physics.SphereCastAll(transform.position, targetRadius,
              transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack && !isHit)
            StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        isChase = false;
        isAttack = true;

        anim.SetTrigger("attackReady");
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("isAttack", true);

        switch (enemyType)
        {
            case Type.Slime:
                if (isHit) {
                    anim.SetBool("isAttack", false);
                    isAttack = false;
                    break;
                }
                yield return new WaitForSeconds(0.3f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = false;

                anim.SetBool("isAttack", false);
                // yield return new WaitForSeconds(2f);

                break;
        }

        // anim.SetBool("isWalk", true);
        isChase = true;
        isAttack = false;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "sword" && !isHit)
        {
            Vector3 reactVec = transform.position - collider.transform.position;
            reactVec.y = asd;
            Debug.Log(reactVec);
            curHealth -= 1;

            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec) {
        // 몬스터가 데미지를 받는 계산 부분
        // mat.color = Color.red;
        isHit = true;
        nav.enabled = false;
        yield return new WaitForSeconds(0.1f);

        reactVec = reactVec.normalized;

        rigid.AddForce(reactVec.normalized * 8, ForceMode.Impulse);
        
        if (curHealth > 0) {
            mat.color = Color.white;
            anim.SetTrigger("hit");

            yield return new WaitForSeconds(1f);
            isHit = false;
            nav.enabled = true;

        } else {
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");
            
            Destroy(gameObject, 2);
        }

        
    }
   
}
