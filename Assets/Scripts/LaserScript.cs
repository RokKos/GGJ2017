using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    float laserSpeed = 3f;
	
	// Update is called once per frame
	void Start () {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        float step = laserSpeed * Time.deltaTime;
        transform.position += transform.up * step;
    }
}
