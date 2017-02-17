//Author: Rok Kos <kosrok97@gmail.com>
//File: BasePowerUp.cs
//File path: /D/Documents/Unity/GGJ2017/BasePowerUp.cs
//Date: 17.02.2017
//Description: Class that represents explosion power up
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPowerUp : BasePowerUp {


    protected override void powerUpPickUp (GameObject player) {
        StartCoroutine(explosion(player));
    }

    public override void loadImage () {
        gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("item-killall");
    }

    protected override void playPickUpSound () {
        StartCoroutine(pickUpSound(2));
    }

    public IEnumerator explosion (GameObject player) {
        this.GetComponent<SpriteRenderer>().enabled = false;
        // Enable particle emmision
        ParticleSystem particleSystem = this.GetComponent<ParticleSystem>();

        // Get speed of the game so that you can adapt speed of duration
        //Movement1 movement = player.GetComponent<Movement1>();
        //if (movement.maxTimeSpeed == Time.timeScale) {
        //    particleSystem.main.simulationSpeed  = 1.0f / movement.maxTimeSpeed;
        //} else {
        //    particleSystem.main.simulationSpeed = 1.0f;
        //}
        
        particleSystem.Play();
        //Destroy(this.gameObject, particleSystem.main.duration);

        // Getting reference
        SpawningEnemies se = FindObjectOfType<SpawningEnemies>();
        GameObject[] allEnemies = se.allEnemies;
        int numberOfEnemies = se.currNumberOfEnemies;
        float placeX = se.SIZEOFBOX_X;
        float placeY = se.SIZEOFBOX_Y;
        // Place all enemies in dead zone so that wave is over and they can respawn again
        for (int i = 0; i < numberOfEnemies; ++i) {
            allEnemies[i].SetActive(false);
            allEnemies[i].transform.position = new Vector3(placeX + 1, placeY + 1, allEnemies[i].transform.position.z);
            allEnemies[i].SetActive(true);
        }

        yield return null;
    }
}
