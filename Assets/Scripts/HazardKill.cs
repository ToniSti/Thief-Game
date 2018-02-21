using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("PLAYER HIT HAZARD: " + transform.name);
            collision.transform.GetComponent<PlayerMovement>().Die();
        }
    }

}
