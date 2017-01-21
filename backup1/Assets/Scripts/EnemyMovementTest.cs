using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementTest : MonoBehaviour {

    private Vector3 target;

    void Start()
    {
        target = new Vector3(0, 0, 0);
    }


    void Update()
    {
        calculateAngle();
        moveToPosition();
    }

    private void moveToPosition()
    {
        float step = 2f * Time.deltaTime;
        transform.position += transform.up * step;
    }

    void calculateAngle()
    {
        Vector3 newDir = target - transform.position;
        float angle = Vector3.Angle(newDir, transform.up);  //calculate angle

        if (angle < 90)
        {
            if (Vector3.Cross(newDir, transform.up).z < 0)
            {
                if (angle > 8)
                    transform.Rotate(Vector3.forward, 8f);
                else
                    transform.Rotate(Vector3.forward, angle);
            }
            else
            {
                if (angle > 8)
                    transform.Rotate(Vector3.forward, -8f);
                else
                    transform.Rotate(Vector3.forward, -angle);
            }
        }
    }
}
