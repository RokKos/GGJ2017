using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTester : MonoBehaviour {

    bool moveUp = true;
    Vector3 newPosition;

	// Use this for initialization
	void Start () {
        newPosition = new Vector3(transform.position.x, 5, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position == newPosition)
            changePosition();

        transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime);

    }

    void changePosition()
    {
        newPosition = new Vector3(newPosition.x, -newPosition.y, 0);
    }
}
