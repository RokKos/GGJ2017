﻿//Author: Rok Kos <kosrok97@gmail.com>
//File: BasePowerUp.cs
//File path: /D/Documents/Unity/GGJ2017/BasePowerUp.cs
//Date: 03.02.2017
//Description: Class that represents all power ups

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour {

    protected float timeLasting;
    protected AudioSource audioSource;
    // Clips:
    // 0: Big Enemy power up
    // 1: Shiled power up
    // 2: Explosion power up
    // 3: Miror power up
    protected AudioClip[] clips;

    private void Start () {
        Debug.Log("Created");
        audioSource = this.gameObject.GetComponent<AudioSource>();
        clips = (AudioClip[])Resources.LoadAll<AudioClip>("PowerUpSounds/");
    }

    // This will happen for every power up
    private void OnCollisionEnter2D (Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
            powerUpPickUp(coll.gameObject);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        // Plays diffrend sound for every power up
        playPickUpSound();
    }

    // Function that is diffrend for every power up
    protected abstract void powerUpPickUp (GameObject player);

    public abstract void loadImage ();

    protected abstract void playPickUpSound ();

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

    protected IEnumerator pickUpSound (int clipIndex) {
        Debug.Log("Here");
        audioSource.volume = 0.3f;
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
        while (audioSource.isPlaying) {
            yield return null;
        }
        audioSource.volume = 0.1f;
        audioSource.Stop();
        yield return null;
    }



}
