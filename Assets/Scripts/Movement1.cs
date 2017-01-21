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
            //transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            //Vector3 newDir = Vector3.RotateTowards(transform.position, newPosition, step, 0.0F);


            transform.position += transform.up * step;


            //transform.Rotate(newDir);
            //transform.rotation = Quaternion.LookRotation(newDir);
        }
        else {  //drugače upočasnimo čas in dovolimo novi cilj
            Time.timeScale = minTimeSpeed;
            allowNewPosition = true;
            //Debug.Log("No move");
        }

        calculateAngle();
    }

    private void checkClick()
    {
        //preverimo klik za novo pozicijo
        if (Input.GetMouseButtonDown(0)) //&& allowNewPosition)
        {
            //Debug.Log("klik");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("rayCastHit");
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

    void calculateAngle()
    {
        Vector3 newDir = newPosition - transform.position;
        float angle = Vector3.Angle(newDir, transform.up);  //calculate angle

        if (angle > 5 || angle < -5)
        {
            if (Vector3.Cross(newDir, transform.up).z < 0)
            {
                //angle = -angle;
                transform.Rotate(Vector3.forward, 10f);
            }
            else
            {
                transform.Rotate(Vector3.forward, -10f);
            }

        }
        //Debug.Log(angle);
    }
}
