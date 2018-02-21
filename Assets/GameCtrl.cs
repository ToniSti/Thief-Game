using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameCtrl : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator DeathWait()
    {
        Debug.Log("RESTARTING...");
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("RESET");
        Reset();
    }
}
