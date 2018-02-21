using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    public Transform groundCheck;
    public LayerMask ground, floor;
    public float hor, ver, speed, jumpForce, groundRadius;
    public bool alive, jumping, dJump, climbing, attacking, grounded;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        alive = true;
	}
	
	void Update ()
    {
        if (!attacking && !jumping)
        {
            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");
        }
        
        if (hor < 0)
        {
            sr.flipX = true;
        }
        else if (hor > 0)
        {
            sr.flipX = false;
        }

        if(Input.GetButtonDown("Jump") && !climbing)
        {
            if (!jumping && grounded)
            {
                Jump();
            }
            else if (jumping && !dJump)
            {
                dJump = true;
                Jump();
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            attacking = true;
        }


        if(attacking)
        {
            anim.Play("PlayerHit");
        }
        else if(hor != 0)
        {
            anim.Play("PlayerWalk");
        }
        else if(hor == 0)
        {
            anim.Play("PlayerIdle");
        }
	}

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundRadius))
        {
            grounded = true;
            jumping = false;
            dJump = false;
        }
        else
        {
            grounded = false;
            jumping = true;
        }

        if (alive)
        {
            if (climbing)
            {
                rb.velocity = new Vector2(hor, ver) * speed;
            }
            else
            {
                rb.velocity = new Vector2(hor * speed, rb.velocity.y);
            }
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpForce));
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("ENEMY IN RANGE");

            if (attacking == true)
            {
                Debug.Log("ENEMY HIT");

                if (sr.flipX == false && collision.transform.localScale.x == 1)
                {
                    Debug.Log("BACKSTAB");
                }
                else if (sr.flipX == true && collision.transform.localScale.x == -1)
                {
                    Debug.Log("BACKSTAB");
                }
                else
                {
                    Debug.Log("FRONTSTAB");
                }
            }
        }
    }

    public void Attack()
    {
        Debug.Log("ATTACK");
        attacking = false;
    }

    public void Die()
    {
        alive = false;
        rb.velocity = Vector2.zero;
        sr.enabled = false;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCtrl>().StartCoroutine("DeathWait");
    }
}
