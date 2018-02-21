using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {

    public bool active = false, playerIn = false;
    public float triggerSpeed, rechargeSpeed;
    public Sprite[] sprites;
    SpriteRenderer sr;

	void Start ()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprites[0];
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerIn = true;
            Debug.Log("!!TRAP HIT!!");

            if (!active)
            {
                StartCoroutine("Activate", collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIn = false;
        }
    }

    void CheckPlayer(GameObject player)
    {
        sr.sprite = sprites[1];

        if(active && playerIn)
        {
            Debug.Log("!!PLAYER HIT!!");
            player.GetComponent<PlayerMovement>().Die();
        }
        else
        {
            Debug.Log("!!TRAP MISSED!!");
        }

        StartCoroutine("ReActivate");
    }

    IEnumerator Activate(GameObject player)
    {
        active = true;
        yield return new WaitForSecondsRealtime(triggerSpeed);
        Debug.Log("!!TRAP ACTIVATED!!");
        CheckPlayer(player);
    }

    IEnumerator ReActivate()
    {
        yield return new WaitForSecondsRealtime(rechargeSpeed);
        Debug.Log("!!TRAP READY!!");
        sr.sprite = sprites[0];
        active = false;
    }
}
