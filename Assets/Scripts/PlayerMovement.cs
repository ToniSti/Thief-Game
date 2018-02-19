using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody2D rb;
    SpriteRenderer sr;
    public Transform groundCheck;
    public LayerMask ground, floor;
    public float hor, ver, speed, jumpForce, groundRadius;
    public bool jumping, dJump, climbing, grounded;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update ()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        if(hor < 0)
        {
            sr.flipX = true;
        }
        else if(hor > 0)
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
	}

    void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground) || Physics2D.OverlapCircle(groundCheck.position, groundRadius, floor))
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

        if (climbing)
        {
            rb.velocity = new Vector2(hor, ver) * speed;
        }
        else
        {
            rb.velocity = new Vector2(hor * speed, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpForce));
    }

    public IEnumerator Flattened()
    {
        Debug.Log("!!PLAYER FLATTENED!!");
        sr.transform.localScale = new Vector2(1, 0.5f);
        speed = speed / 2;
        yield return new WaitForSecondsRealtime(2f);
        sr.transform.localScale = new Vector2(1, 1);
        speed = speed * 2;
        Debug.Log("!!PLAYER RECOVERED!!");
    }
}
