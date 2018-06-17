using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "MainFloor" || other.tag == "Player")
        {
            return;
        }

        // destroy parent if object has one, else destroy only the object
        if (other.gameObject.transform.parent)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
	}
}
