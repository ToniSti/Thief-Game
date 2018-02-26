using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbController : MonoBehaviour
{
    public bool climbing;
    public float gravitySave;
    public GameObject player;
    Rigidbody2D playerRB;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        gravitySave = playerRB.gravityScale;
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && climbing)
        {
            StartCoroutine("StopClimb");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetAxisRaw("Vertical") != 0 && !climbing)
        {
            climbing = true;
            player.GetComponent<PlayerMovement>().climbing = true;
            player.transform.position = new Vector2(transform.position.x, collision.transform.position.y);
            playerRB.gravityScale = 0;
            playerRB.velocity = Vector2.zero;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && climbing)
        {
            StartCoroutine("StopClimb");
        }
    }

    IEnumerator StopClimb()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        climbing = false;
        player.GetComponent<PlayerMovement>().climbing = false;
        playerRB.gravityScale = gravitySave;
        playerRB.velocity = Vector2.zero;
    }
}
