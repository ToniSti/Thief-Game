using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
	void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" && Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("End");
        }
    }
}
