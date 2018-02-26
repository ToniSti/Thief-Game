using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    PlayerAttack pa;
    public bool alreadyHit;

    void start()
    {
        pa = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    public void TakeHit(bool kill)
    {
        if (!alreadyHit)
        {
            alreadyHit = true;

            if (kill)
            {
                Debug.Log(transform.gameObject.name + " BACKSTABBED! >:D");
                Die();
            }
            else
            {
                Debug.Log(transform.gameObject.name + " FRONTSTABBED! :(");
                StartCoroutine("Invulnerable");
            }
        }
    }

    public void Die()
    {
        Destroy(transform.gameObject);
    }

    IEnumerator Invulnerable()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        alreadyHit = false;
    }
}
