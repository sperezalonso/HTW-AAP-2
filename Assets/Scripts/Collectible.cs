using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    Camera camera;
    float speed;

    void Start()
    {
        camera = Camera.main;
        speed = Random.Range(6, 15);    
    }

    void Update () {
        transform.Rotate(new Vector3(speed, speed * 2, speed * 3) * Time.deltaTime);	    //randomly rotate the object
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            camera.GetComponent<HUD>().IncreaseScore(10);
            Destroy(this.gameObject);
        }
    }
}
