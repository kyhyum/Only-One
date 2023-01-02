using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //데미지 텍스트 -----
    public GameObject DamageText;
    public Transform textPos;
    //-------------------
    public enum Type { Slime, Turtle, Lich, Grunt };
    public Type enemyType;
    public Transform target;
    public BoxCollider meleeArea;

    public ParticleSystem Stuned;

    public GameObject bullet;

    public int maxHealth;
    public int curHealth;

    public bool isChase;
    public bool isAttack;
    public bool isHit;
    public bool isDefence = false;

    GameObject Player;

    int dRand;

    Rigidbody rigid;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        target = Player.GetComponent<Transform>();
        Stuned.Stop();

        textPos = transform.Find("damage_textpos").transform;

        Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }

    void FreezeVelocity()
    {
        if (isChase)
        {

            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    void Targeting()
    {
        float targetRadius = 0;
        float targetRange = 0;

        switch (enemyType)
        {
            case Type.Slime:
                targetRadius = 0.8f;
                targetRange = 1.2f;
                break;

            case Type.Turtle:
                targetRadius = 0.8f;
                targetRange = 1.5f;
                break;

            case Type.Lich:
                targetRadius = 0.5f;
                targetRange = 12f;
                break;

            case Type.Grunt:
                targetRadius = 0.7f;
                targetRange = 6f;
                break;


        }

        RaycastHit[] rayHits
             = Physics.SphereCastAll(transform.position, targetRadius,
              transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack && !isHit && !isDefence)
            StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;

        switch (enemyType)
        {
            case Type.Slime:
                anim.SetTrigger("attackReady");
                yield return new WaitForSeconds(0.8f);
                anim.SetBool("isAttack", true);

                if (isHit)
                {
                    anim.SetBool("isAttack", false);
                    isAttack = false;
                    break;
                }

                yield return new WaitForSeconds(0.3f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = false;

                anim.SetBool("isAttack", false);
                anim.SetBool("isWalk", false);
                yield return new WaitForSeconds(0.5f);

                break;

            case Type.Turtle:
                anim.SetTrigger("attackReady");
                dRand = Random.Range(0, 3);
                isDefence = dRand == 0 ? true : false;
                if (isDefence)
                {
                    anim.SetBool("isDefence", true);

                    yield return new WaitForSeconds(2f);
                    anim.SetBool("isDefence", false);
                    anim.SetBool("isWalk", false);
                    isDefence = false;
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                    anim.SetBool("isAttack", true);

                    if (isHit)
                    {
                        anim.SetBool("isAttack", false);
                        isAttack = false;
                        break;
                    }

                    yield return new WaitForSeconds(0.5f);
                    meleeArea.enabled = true;

                    yield return new WaitForSeconds(0.2f);
                    meleeArea.enabled = false;

                    anim.SetBool("isAttack", false);
                    anim.SetBool("isWalk", false);
                    yield return new WaitForSeconds(0.5f);

                    break;
                }

            case Type.Lich:
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("isAttack", true);

                yield return new WaitForSeconds(0.4f);

                // 투사체
                /* GameObject instantBullet = Instantiate(bullet, transform.position - (Vector3.forward), transform.rotation);
                 Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                 rigidBullet.velocity = transform.forward * 6;*/
                spwaner.Instance.lich_attack(this.gameObject);
                 // 투사체 파괴
                 //Destroy(instantBullet, 10);

                 yield return new WaitForSeconds(0.5f);
                anim.SetBool("isAttack", false);
                anim.SetBool("isWalk", false);
                yield return new WaitForSeconds(1.5f);

                break;

            case Type Grunt:
                anim.SetBool("isRun", true);
                rigid.AddForce(transform.forward * 8, ForceMode.Impulse);

                if (isHit)
                {
                    anim.SetBool("isRun", false);
                    anim.SetBool("isAttack", false);
                    isAttack = false;
                    meleeArea.enabled = false;
                    break;
                }

                anim.SetBool("isWalk", false);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("isAttack", true);
                meleeArea.enabled = true;
                anim.SetBool("isRun", false);

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;
                anim.SetTrigger("comboAttack");

                yield return new WaitForSeconds(0.5f);
                meleeArea.enabled = true;
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("isAttack", false);

                yield return new WaitForSeconds(0.3f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);

                break;
        }

        anim.SetBool("isWalk", true);
        isChase = true;
        isAttack = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "sword" && !isHit)
        {
            Vector3 reactVec = transform.position - collider.transform.position;
            if (!isDefence)
            {
                curHealth -= Player.GetComponent<Player>().damage;
                GameObject dmgtext = Instantiate(DamageText);
                dmgtext.transform.position = textPos.position;
                dmgtext.GetComponent<damage_text>().damage = Random.Range(10,44);
            }
            reactVec = reactVec.normalized;

            StartCoroutine(OnDamage(reactVec));
        }
        else if (collider.gameObject.tag == "Slash")
        {
            Vector3 reactVec = transform.position - collider.transform.position;
            reactVec = reactVec.normalized;

            StartCoroutine(OnStuned(reactVec*2));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava")
            Destroy(gameObject);
    }

    public IEnumerator OnStuned(Vector3 reactVec)
    {
        isHit = true;
        nav.enabled = false;
        yield return new WaitForSeconds(0.1f);

        rigid.AddForce(reactVec * 10, ForceMode.Impulse);
        Stuned.Play();

        anim.SetTrigger("hit");

        yield return new WaitForSeconds(1.5f); 
        Stuned.Stop();

        isHit = false;
        if (transform.position.y < 0)
        {
            isChase = false;
            nav.enabled = false;
        }
        else
        {
            nav.enabled = true;
        }
    }

    public IEnumerator OnDamage(Vector3 reactVec)
    {
        // 몬스터가 데미지를 받는 계산 부분
        isHit = true;
        nav.enabled = false;
        yield return new WaitForSeconds(0.1f);

        rigid.AddForce(reactVec * (isDefence ? 2 : 4), ForceMode.Impulse);

        if (curHealth > 0)
        {
            anim.SetTrigger("hit");

            yield return new WaitForSeconds(0.3f);
            isHit = false;
            if (transform.position.y < 0)
            {
                isChase = false;
                nav.enabled = false;
            }
            else
            {
                nav.enabled = true;
            }
        }
        else
        {
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");
            yield return new WaitForSeconds(1f);
            spwaner.Instance.Die(this, enemyType.ToString());
        }


    }

}