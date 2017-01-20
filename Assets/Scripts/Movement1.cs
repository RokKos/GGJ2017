using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour {

    public float playerSpeed;
    public float minTimeSpeed = 0.5f;
    public float maxTimeSpeed = 2f;
    private Vector3 newPosition;
    private bool allowNewPosition;
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        newPosition = transform.position;
        Time.timeScale = minTimeSpeed;
        allowNewPosition = true;
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        checkClick();
        moveToPosition();
    }

    private void moveToPosition()
    {
        //se premaknemo če še nismo dosegli cilj
        if (transform.position != newPosition)
        {
            Time.timeScale = maxTimeSpeed;
            float step = playerSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        }
        else {  //drugače upočasnimo čas in dovolimo novi cilj
            Time.timeScale = minTimeSpeed;
            allowNewPosition = true;
            //Debug.Log("No move");
        }
    }

    private void checkClick()
    {
        //preverimo klik za novo pozicijo
        if (allowNewPosition && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("klik");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("rayCastHit");
                newPosition = hit.point;
                allowNewPosition = false;
                //transform.position = newPosition;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision detetcted, rigidbody set to kinematic. END GAME");
            rigidBody.isKinematic = true;

        }

    }
}
