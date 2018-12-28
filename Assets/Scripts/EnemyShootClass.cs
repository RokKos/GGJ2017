//Author: Rok Kos <kosrok97@gmail.com>
//File: EnemyCurveClass.cs
//File path: /D/Documents/Unity/GGJ2017/EnemyCurveClass.cs
//Date: 20.01.2017
//Description: Little bit advanced enemy with shoting

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootClass : EnemyCurveClass {
    private float fireRate;
    private float timeSinceLastShoot;
    private Transform transform;
    private Shoot shoot;

    // Constructor 
    public EnemyShootClass (byte _tip, Vector3 _startPos, Vector3 _endPos, float _timeStart, Vector3 _userPosition, float _sizeOfBoxX, float _sizeOfBoxY, float _fireRate, float timeSinceLastShot) :
        base(_tip, _startPos, _endPos, _timeStart, _userPosition, _sizeOfBoxX, _sizeOfBoxY) {
        this.fireRate = _fireRate;

    }

    //public bool fireGun () {
    //    if (Time.time - this.timeSinceLastShoot > fireRate ) {
    //        this.timeSinceLastShoot = Time.time;
    //        return true;
    //    }

    //    return false;
    //}

    public void fireGun()
    {
        if (Time.time - this.timeSinceLastShoot > fireRate)
        {
            this.timeSinceLastShoot = Time.time;

            //trail1.SetActive(false);
            //result.transform.position = startPos;
            //trail1.SetActive(true);
            shoot.Laser1(transform.position, transform.rotation);
        }
    }

    public void setShooter(Transform transform)
    {
        this.transform = transform;
        GameObject shooter = transform.Find("Shooter").gameObject;
        shoot = shooter.GetComponent<Shoot>();
    }

}
