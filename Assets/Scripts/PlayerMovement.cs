//Jonna Majewski kirjoitti alustavan pelaajan liikkumisen ja hyppimisen.
//Toni Stigell lisäsi kiipeämisen, kuoleman ja animaatiot. Lisäksi sääti aiempia osia uusiin sopiviksi
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;
    public Transform groundCheck;
    public LayerMask ground;
    public float hor, ver, speed, jumpForce, groundRadius;
    public bool canMove, jumping, climbing, grounded;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
	}

    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && !climbing && !jumping && grounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground))
        {
            grounded = true;
            jumping = false;
        }
        else
        {
            grounded = false;
            jumping = true;
        }

        Move();
    }

    void Move()
    {
        if (canMove)
        {
            anim.SetBool("Climb", climbing);

            if (hor < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (hor > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

        
            if (climbing)
            {
                anim.Play("PlayerClimb");

                rb.velocity = new Vector2(hor, ver) * speed;

                if (ver != 0)
                {
                    anim.speed = 1f;
                }
                else if (climbing && ver == 0)
                {
                    anim.speed = 0f;
                }
            }
            else
            {
                rb.velocity = new Vector2(hor * speed, rb.velocity.y);
                anim.SetFloat("Walk", Mathf.Abs(hor));
                anim.speed = 1f;
            }
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpForce));
    }

    public void Die()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCtrl>().StartCoroutine("DeathWait");
        Destroy(transform.gameObject);
    }

    public void CanMove(bool can)
    {
        canMove = can;

        if(can == false)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
