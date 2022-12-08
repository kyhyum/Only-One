using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public GameObject CharacterMaterial;
    public bl_Joystick js;
    public Button btn;
    public float speed;
    private bool isDie;
    public bool attacked = false;
    public BoxCollider meleeArea;
    Animator animator;
    public Camera playerCamera;
    public bool ishit = false;
    //hp
    public Image[] Heart;
    public int Hp;
    private int maxHp;
    public Sprite Back, Front;
    private SkinnedMeshRenderer rd;
    private Material[] mat;
    private void Start()
    {
        meleeArea.enabled = false;
        rd = CharacterMaterial.GetComponent<SkinnedMeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isDie = false;
        //hp
        maxHp = 5;
        Hp = maxHp;
        for (int i = 0; i < Hp; i++)
            Heart[i].sprite = Front;
        mat = rd.materials;
        CharacterMaterial.GetComponent<SkinnedMeshRenderer>().material = mat[0];
    }
    private void Update()
    {

        Die();
        if (((Mathf.Abs(js.Horizontal)) > 0.01 || (Mathf.Abs(js.Vertical)) > 0.01) && !isDie && !ishit)
        {
            //Debug.Log(js.Horizontal);
            //Debug.Log(js.Vertical);
            Move();
        }
        else
        {
            //Debug.Log("끝");
            animator.SetBool("IsMove", false);
        }
        //Debug.Log(js.Horizontal);
        //Debug.Log(js.Vertical);
        //공격방식 수정
        btn.onClick.AddListener(() =>
        {
            if (!isDie)
                StartCoroutine(Attack());
        });
        /*if(transform.position.y <= -1)
        {
            gameObject.layer = 9;
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Monster") && !isDie && !ishit && gameObject.layer == 6)
        {
            animator.SetTrigger("IsHit");
            Vector3 reactVec = transform.position - collision.transform.position;
            hp_down();
            OnDamaged(reactVec);
        }
        if (collision.gameObject.tag == "Lava")
        {
            while (Hp > 0)
                hp_down();
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
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.position = transform.position + moveVector;
        }

    }

    IEnumerator Attack()
    {
        animator.SetLayerWeight(1, 1);
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("NormalAttack01_SwordShield"))
            animator.SetTrigger("IsAttack");
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
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
        HitTrue();
        this.gameObject.layer = 8;
        rigid.AddForce(reactVec.normalized * 3, ForceMode.Impulse);
        StartCoroutine("blink");
        Invoke("HitFalse", 1);
        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
    }

    public void hp_down()
    {
        if (Hp > 0)
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
    }
    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }

    void HitTrue()
    {
        ishit = true;
    }

    void HitFalse()
    {
        ishit = false;
    }

    IEnumerator blink()
    {
        int countTime = 0;
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
            {
                CharacterMaterial.GetComponent<SkinnedMeshRenderer>().material = mat[0];
            }
            else
            {
                CharacterMaterial.GetComponent<SkinnedMeshRenderer>().material = mat[1];
            }
            yield return new WaitForSeconds(0.2f);
            countTime++;
        }
        CharacterMaterial.GetComponent<SkinnedMeshRenderer>().material = mat[0];
        yield return null;
    }
}