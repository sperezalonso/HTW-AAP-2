using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] obj;
    public float spawnMin = 1.5f;
    public float spawnMax = 3f;

	void Start () {
        Spawn();
	}
	
	void Spawn() {
        Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
