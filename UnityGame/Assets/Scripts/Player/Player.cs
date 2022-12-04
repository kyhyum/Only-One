using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public bl_Joystick js;
    public Button btn;
    public float speed;
    private bool isDie;
    public bool attacked = false;
    Animator animator;
    public Camera playerCamera;
    public bool ishit = false;
    //hp
    public Image[] Heart;
    public int Hp;
    private int maxHp;
    public Sprite Back, Front;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isDie = false;
        //hp
        maxHp = 5;
        Hp = maxHp;
        for (int i = 0; i < Hp; i++)
            Heart[i].sprite = Front;
    }
    private void FixedUpdate()
    {
        Die();
        if ((js.Horizontal != 0 || js.Vertical != 0) && !isDie && !ishit)
        {
            Move();
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
        //공격방식 수정
        btn.onClick.AddListener(() =>
        {
            if(!isDie)
                Attack();
        });
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster" && !isDie)
        {
            animator.SetTrigger("IsHit");
            Vector3 reactVec = transform.position - collision.transform.position;
            hp_down();
            OnDamaged(reactVec);
            
        }
    }
    public void Move()
    {
            Vector3 dir = new Vector3(js.Horizontal, 0, js.Vertical);
            float vecSize = Mathf.Abs(dir.x) + Mathf.Abs(dir.z);
            animator.SetBool("IsMove", true);

            if (vecSize >= 5f)
            {
                animator.SetLayerWeight(1, 1);
                animator.SetBool("IsRun", true);
                speed = 1f;
            }
            else
            {
                animator.SetBool("IsRun", false);
                speed = 1f;
            }
            Vector3 moveVector = dir * speed * Time.deltaTime;
            Quaternion v3Rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
            moveVector = v3Rotation * moveVector;
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.position = transform.position + moveVector;
        
    }

    public void Attack()
    {
        animator.SetLayerWeight(1, 1);
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("NormalAttack01_SwordShield"))
            animator.SetTrigger("IsAttack");
       /* else
        {
            //2타공격
                animator.SetTrigger("isAttack2");
        }*/
    }

    public void Die()
    {
        if (Hp <= 0)
        {
            isDie = true;
            rigid.velocity = Vector3.zero;
            animator.SetLayerWeight(1, 0);
            animator.SetBool("Die", true);
        }
    }
    void OnDamaged(Vector3 reactVec)
    {
        HitTure();
        this.gameObject.layer = 8;
        rigid.AddForce(reactVec.normalized * 10, ForceMode.Impulse);
        Invoke("HitFalse", 1);
        Invoke("OffDamaged", 3);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
    }

    public void hp_down()
    {
        Hp -= 1;
        Hp = Mathf.Clamp(Hp, 0, maxHp);
        for (int i = 0; i < maxHp; i++)
            Heart[i].sprite = Back;

        for (int i = 0; i < maxHp; i++)
            if (Hp > i)
            {
                Heart[i].sprite = Front;
            }
    }
    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }

    void HitTure()
    {
        ishit = true;
    }
    
    void HitFalse()
    {
        ishit = false;
    }
}
