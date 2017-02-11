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
            powerUpPickUp(coll.gameObject);
        }

        // Copy script to player
        coll.gameObject.AddComponent(typeof(BasePowerUp));
    }

    // Function that is diffrend for every power up
    protected abstract void powerUpPickUp (GameObject player);

    protected abstract void loadImage ();

    public Vector3 placePowerUp () {
        float x = 0.0f;
        float y = 0.0f;
        // Find all enemies
        SpawningEnemies spawningEnemies = (SpawningEnemies) FindObjectOfType(typeof(SpawningEnemies));
        GameObject[] enemies = spawningEnemies.allEnemies;
        int N = 0;
        for (int i = 0; i < enemies.Length; ++i) {
            if (enemies[i] != null) {
                x += enemies[i].transform.position.x;
                y += enemies[i].transform.position.y;
                N++;
            }
            
        }

        return new Vector3(x / N, y / N, 0.0f);
    }



}
