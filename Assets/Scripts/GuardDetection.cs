using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetection : MonoBehaviour {

    GuardController gc;
    public float lineOffset, angle, outOfSightTime, outOfSightTimeBase;
    public bool leftSight, chasing;

    void Start ()
    {
        gc = GetComponent<GuardController>();
	}

    void FixedUpdate()
    {
        if(leftSight && chasing)
        {
            outOfSightTime -= 1f;

            if(outOfSightTime <= 0)
            {
                leftSight = false;
                chasing = false;
                gc.speed = gc.speedBase;
                gc.Chase(false);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("PLAYER WITHIN RANGE");

            RaycastHit2D hit = Physics2D.Raycast(transform.position, collision.transform.position - transform.position);
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawRay(transform.position, collision.transform.position + new Vector3(0, lineOffset, 0) - transform.position);

            angle = Mathf.Atan2(transform.position.y - collision.transform.position.y - lineOffset, transform.position.x - collision.transform.position.x) * Mathf.Rad2Deg;

            if(transform.localScale.x == -1 && (angle >= -40 && angle <= 40))
            {
                Debug.Log("PLAYER SEEN");
                leftSight = false;
                gc.speed = gc.speedBase + 1;
                outOfSightTime = outOfSightTimeBase;
                chasing = true;
                gc.Chase(true);
            }
            else if (transform.localScale.x == 1 && (angle <= -140 || angle >= 140))
            {
                Debug.Log("PLAYER SEEN");
                leftSight = false;
                outOfSightTime = outOfSightTimeBase;
                chasing = true;
                gc.Chase(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("PLAYER LEFT RANGE");
            if (chasing)
            {
                leftSight = true;
            }
        }
    }
}
