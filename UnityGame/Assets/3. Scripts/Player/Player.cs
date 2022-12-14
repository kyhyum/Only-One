using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public GameObject CharacterMaterial;
    public bl_Joystick js;
    public Button attackbtn;
    public Button rollingbtn;

    public PopupScript popupscript;
    //public Money money;

    public float speed;
    private bool isDie;
    public bool attacked = false;
    public BoxCollider meleeArea;
    Animator animator;
    public Camera playerCamera;
    public bool ishit = false;
    public UserHp userhp;
    private SkinnedMeshRenderer rd;
    private Material[] mat;

    private void Start()
    {
        meleeArea.enabled = false;
        rd = CharacterMaterial.GetComponent<SkinnedMeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isDie = false;
        mat = rd.materials;
        CharacterMaterial.GetComponent<SkinnedMeshRenderer>().material = mat[0];
    }
    private void Update()
    {
        attackbtn.onClick.AddListener(() =>
        {
            if (!isDie)
                StartCoroutine(Attack());
        });
        rollingbtn.onClick.AddListener(() =>
        {
            if (!isDie)
                StartCoroutine(Rolling());

        });
        Die();
        if (((Mathf.Abs(js.Horizontal)) > 0.01 || (Mathf.Abs(js.Vertical)) > 0.01) && !isDie && !ishit)
        {
            Move();
        }
        else
        {
            animator.SetBool("IsMove", false);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Monster Bullet") && !isDie && !ishit && gameObject.layer == 6)
        {
            animator.SetTrigger("IsHit");
            if (collision.gameObject.tag == "Monster Bullet")
                Destroy(collision.gameObject);
            Vector3 reactVec = transform.position - collision.transform.position;
            userhp.hp_down();
            OnDamaged(reactVec);
        }
        if (collision.gameObject.tag == "Lava")
        {
            for (int i = 0; i < 5; i++)
                userhp.hp_down();
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
    IEnumerator Rolling()
    {
        animator.SetTrigger("isrolling");
        this.gameObject.layer = 8;
        yield return new WaitForSeconds(1f);
        animator.SetBool("isrolling", false);
        HitFalse();
        OffDamaged();
    }

    public void Die()
    {
        if (userhp.Hp <= 0)
        {
            isDie = true;
            rigid.velocity = Vector3.zero;
            animator.SetLayerWeight(1, 0);
            animator.SetBool("Die", true);
            //money.Money_save();
            Invoke("popup", 2);
        }
    }
    void popup()
    {
        popupscript.PopupActive();
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