using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;

    Vector3 offset;     // offset distance between player and camera

	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;

        // freeze the camera's Y position
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
	}
}
