//Author: Rok Kos <kosrok97@gmail.com>
//File: BasePowerUp.cs
//File path: /D/Documents/Unity/GGJ2017/BasePowerUp.cs
//Date: 03.02.2017
//Description: Class that represents all power ups

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour {

    protected float timeLasting;

    // Contructor
    public BasePowerUp (float _timeLasting) {
        this.timeLasting = _timeLasting;
        Destroy(gameObject, timeLasting);

        // Load diffrent image in diffrent position on screen
        loadImage();

        Debug.Log("Create");
    }

    // Destructor
    ~BasePowerUp () {

    }

    // This will happen for every power up
    private void OnCollisionEnter2D (Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            powerUpPickUp();
        }
    }

    // Function that is diffrend for every power up
    protected abstract void powerUpPickUp ();

    protected abstract void loadImage ();



}
