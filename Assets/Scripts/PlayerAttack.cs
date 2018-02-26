using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    PlayerMovement pm;
    public bool attacking, stab;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !attacking && !pm.jumping && !pm.climbing)
        {
            attacking = true;
            pm.CanMove(false);
        }

        if(attacking)
        {
            anim.speed = 1f;
            anim.SetBool("Attack", true);
        }
        else if(!attacking)
        {
            anim.SetBool("Attack", false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("ENEMY IN RANGE");

            if (attacking && stab)
            {
                Debug.Log("ENEMY HIT");

                if (transform.localScale.x == collision.transform.localScale.x)
                {
                    Debug.Log("BACKSTAB");
                    collision.gameObject.GetComponent<EnemyHealth>().TakeHit(true);
                }
                else
                {
                    Debug.Log("FRONTSTAB");
                    collision.gameObject.GetComponent<EnemyHealth>().TakeHit(false);
                }
            }
        }
    }

    public void Stab()
    {
        stab = true;
    }

    public void StopAttack()
    {
        attacking = false;
        stab = false;
        pm.CanMove(true);
    }
}
