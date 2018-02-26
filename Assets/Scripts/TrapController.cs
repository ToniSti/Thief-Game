using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {

    Animator anim;

    public bool active = false, playerIn = false;
    public float triggerSpeed, rechargeSpeed;
    public Sprite[] sprites;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerIn = true;
            Debug.Log("!!TRAP HIT!!");

            if (!active)
            {
                Debug.Log("!!TRAP ACTIVATED!!");
                active = true;
                anim.SetBool("Active", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("!!TRAP LEFT!!");
            playerIn = false;
        }
    }

    public void CheckPlayer()
    {
        if(playerIn)
        {
            Debug.Log("!!PLAYER HIT!!");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Die();
        }
        else
        {
            Debug.Log("!!TRAP MISSED!!");
        }
    }

    public void TrapReady()
    {
        Debug.Log("!!TRAP READY!!");
        anim.SetBool("Active", false);
        active = false;
    }
}
