using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bullet;

	public void Laser1(Vector3 position, Quaternion rotation)
    {
        //Debug.Log("IMMA SHOOTIN' MAH LAZER!!1");
        GameObject laser = (GameObject)Instantiate(bullet, position, rotation);
    }
}
