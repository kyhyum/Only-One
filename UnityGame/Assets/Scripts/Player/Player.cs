using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public Collider col;
    public float speed;
    public float hp;
    private bool isDie;
    Vector3 lookDirection;
    Animator animator;

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
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && !isDie)
        {
            Move();
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
        if (Input.GetMouseButtonDown(0) && !isDie)
        {
            Attack();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            animator.SetTrigger("IsHit");
            hp -= 10f;
            OnDamaged(collision.transform.position);
        }
    }
    public void Move()
    {
        float xx = Input.GetAxisRaw("Vertical");
        float zz = Input.GetAxisRaw("Horizontal");
        lookDirection = xx * Vector3.forward + zz * Vector3.right;

        animator.SetBool("IsMove", true);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 1);
            animator.SetBool("IsRun", true);
            speed = 7f;
        }
        else
        {
            animator.SetBool("IsRun", false);
            speed = 5f;
        }
        transform.rotation = Quaternion.LookRotation(lookDirection);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Attack()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("IsAttack");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack01_SwordShield") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            animator.SetTrigger("IsAttack2");
        }
        else
            animator.SetTrigger("NotAttack");
    }

    public void Die()
    {
        if(hp <= 0)
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
