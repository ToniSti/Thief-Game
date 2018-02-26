using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardKill : MonoBehaviour
{
    public bool deadly = true, canKillEnemy;

    public void SetDeadly()
    {
        if (!deadly)
        {
            deadly = true;
        }
        else if(deadly)
        {
            deadly = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" && deadly)
        {
            Debug.Log("PLAYER KILLED BY HAZARD: " + transform.name);
            collision.transform.GetComponent<PlayerMovement>().Die();
        }
        else if(collision.transform.tag == "Enemy"&& canKillEnemy && deadly)
        {
            Debug.Log(collision.transform.name + " KILLED BY HAZARD: " + transform.name);
            collision.gameObject.GetComponent<EnemyHealth>().Die();
        }
    }
}
