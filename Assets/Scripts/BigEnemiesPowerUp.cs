//Author: Rok Kos <kosrok97@gmail.com>
//File: BigEnemiesPowerUp.cs
//File path: /D/Documents/Unity/GGJ2017/BigEnemiesPowerUp.cs
//Date: 03.02.2017
//Description: Class that represents big enemies power up

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemiesPowerUp : BasePowerUp {

    protected override void powerUpPickUp (GameObject player) {
        StartCoroutine(makeEnemiesBigAgain(10.0f));
    }

    public override void loadImage () {
        gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("item-bigenemy");
    }

    protected override void playPickUpSound () {
        StartCoroutine(pickUpSound(0));
    }

    public IEnumerator makeEnemiesBigAgain (float time) {       
        // Getting reference
        SpawningEnemies se = FindObjectOfType<SpawningEnemies>();
        GameObject[] allEnemies = se.allEnemies;
        int numberOfEnemies = se.currNumberOfEnemies;
        // Vector so that I can later return them bact to previus size
        Vector3[] previus = new Vector3[numberOfEnemies]; 
        for (int i =0; i < numberOfEnemies; ++i) {
            previus[i] = allEnemies[i].transform.localScale;
            allEnemies[i].transform.localScale = new Vector3(previus[i].x * 2, previus[i].y * 2, previus[i].z * 2);
        }

        float timePassed = 0;
        while (timePassed < time) {
            yield return null;
        }

        for (int i = 0; i < numberOfEnemies; ++i) {
            allEnemies[i].transform.localScale = new Vector3(previus[i].x, previus[i].y, previus[i].z);
        }

    }

}