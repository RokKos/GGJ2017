using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour {

    public float speed;
    //public float timeSpeed;
    private Vector3 newPosition;

	// Use this for initialization
	void Start () {
        newPosition = transform.position;
        Time.timeScale = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        checkClick();
        moveToPosition();
    }

    private void moveToPosition()
    {
        if (transform.position != newPosition)
        {
            Time.timeScale = 1f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        }
        else {
            Time.timeScale = 0f;
            //Debug.Log("No move");
        }
    }

    private void checkClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("klik");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("rayCastHit");
                newPosition = hit.point;
                //transform.position = newPosition;
            }
        }
    }
}
