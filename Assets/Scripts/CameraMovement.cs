using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    public Vector3 minLoc, maxLoc;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minLoc.x, maxLoc.x),
            Mathf.Clamp(transform.position.y, minLoc.y, maxLoc.y),
            Mathf.Clamp(transform.position.z, minLoc.z, maxLoc.z));
	}
}
