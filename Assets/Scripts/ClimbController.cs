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

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetAxisRaw("Vertical") != 0 && !climbing)
        {
            climbing = true;
            player.GetComponent<PlayerMovement>().climbing = true;
            player.transform.position = new Vector2(transform.position.x, collision.transform.position.y);
            playerRB.gravityScale = 0;
            playerRB.velocity = Vector2.zero;
            Physics2D.IgnoreLayerCollision(9, 10, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            climbing = false;
            player.GetComponent<PlayerMovement>().climbing = false;
            playerRB.gravityScale = gravitySave;
            Physics2D.IgnoreLayerCollision(9, 10, false);
        }
    }
}
