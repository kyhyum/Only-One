using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Skill : MonoBehaviour
{
    public GameObject Slash;
    Animator animator;

    Transform player_position;
    // Start is called before the first frame update
    void Start()
    {
        player_position = this.GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    public void Skill_Slash()
    {
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("NormalAttack01_SwordShield"))
            animator.SetTrigger("IsAttack");
        GameObject instantSlash = Instantiate(Slash, player_position.position + Vector3.up, player_position.rotation);
        Rigidbody rigidSlash = instantSlash.GetComponent<Rigidbody>();
        rigidSlash.velocity = player_position.forward * 6;
        Destroy(instantSlash, 0.3f);
    }
    void Update()
    {
        
    }
}
