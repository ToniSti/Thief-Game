using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetection : MonoBehaviour {

    GuardController gc;

    void Start ()
    {
        gc = GetComponent<GuardController>();
	}

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("PLAYER WITHIN RANGE");

            RaycastHit2D hit = Physics2D.Raycast(transform.position, collision.transform.position - transform.position);
            Debug.Log(hit.collider.gameObject.name);

            if (hit.transform.tag == "Player" && !gc.chasing)
            {
                Debug.Log("PLAYER SEEN");
                gc.Chase(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("PLAYER LEFT RANGE");
            gc.Chase(false);
        }
    }
}
