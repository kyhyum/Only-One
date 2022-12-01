using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public Collider col;
    public bl_Joystick js;
    public Button btn;
    public float speed;
    public float hp;
    public bool isAttack = false;
    private bool isDie;
    Vector3 lookDirection;
    Animator animator;
    public Camera playerCamera;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        hp = 100f;
        isDie = false;
    }
    private void FixedUpdate()
    {
        Die();
        if ((js.Horizontal != 0 || js.Vertical != 0) && !isDie)
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
                Attack();
        });
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster" && !isDie)
        {
            animator.SetTrigger("IsHit");
            hp -= 10f;
            OnDamaged(collision.transform.position);
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
        Vector3 moveVector =  dir * speed * Time.deltaTime;
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
        if (hp <= 0)
        {
            isDie = true;
            rigid.velocity = Vector3.zero;
            animator.SetLayerWeight(2, 1);
            animator.SetBool("Die", true);
        }
    }
    void OnDamaged(Vector3 targetPos)
    {
        int time = 0;
        this.gameObject.layer = 8;
        Invoke("OffDamaged", 3);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
    }
}
