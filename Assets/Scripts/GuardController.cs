using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    Animator anim;
    public Vector2 target, lastTarget;
    public float speed, speedBase, waitTime;
    public int curPoint;
    public bool waiting, chasing;
    public GameObject[] patrolPoints;

    void Start ()
    {
        speed = speedBase;
        target = patrolPoints[curPoint].transform.position;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(chasing)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
    void FixedUpdate ()
    {
        if (transform.position.x != target.x)
        {
            anim.Play("GuardWalk");
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), speed * Time.deltaTime);
        }
        else if(transform.position.x == target.x && !chasing)
        {
            anim.Play("GuardIdle");
            if (!waiting)
            {
                waiting = true;
                StartCoroutine("Stop");
            }
        }

        if (target.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (target.x > transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void Patrol(bool next)
    {
        if (next)
        {
            if (curPoint < patrolPoints.Length - 1)
            {
                curPoint++;
            }
            else
            {
                curPoint = 0;
            }
        }

        target = patrolPoints[curPoint].transform.position;

        waiting = false;
    }

    public void Chase(bool chase)
    {
        if(chase == true)
        {
            chasing = true;
            lastTarget = patrolPoints[curPoint].transform.position;
        }
        else
        {
            chasing = false;
            target = lastTarget;
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Patrol(true);
    }
}
