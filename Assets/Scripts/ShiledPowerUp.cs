//Author: Rok Kos <kosrok97@gmail.com>
//File: ShieldPowerUp.cs
//File path: /D/Documents/Unity/GGJ2017/ShieldPowerUp.cs
//Date: 03.02.2017
//Description: Class that represents shiled power up

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : BasePowerUp {

    // Contructor
    public ShieldPowerUp (float _timeLasting) : base (_timeLasting) {

        
    }

    // Destructor
    ~ShieldPowerUp () {

    }

    protected override void powerUpPickUp () {
        Debug.Log("Piw piw");

    }

    protected override void loadImage () {
        gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("enemy1");
    }

}