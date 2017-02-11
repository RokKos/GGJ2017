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

    protected override void powerUpPickUp (GameObject player) {
        Debug.Log("Piw piw");
        player.GetComponent<Movement1>().playerLifes = 2;  // For now is on 2 maybe later player could have more lives
        StartCoroutine(glowingShield(player));
        enableShield(player, true);
    }

    protected override void loadImage () {
        gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("enemy1");
    }

    public IEnumerator glowingShield (GameObject player) {
        GameObject sprite = new GameObject();
        /// Find shield
        for (int i = 0; i < player.transform.childCount; ++i) {
            if (player.transform.GetChild(i).name == "Shield") {
                // disable shield
                sprite = player.transform.GetChild(i).gameObject;
            }
        }

        // Animate shield
        float grad = 0.01f;
        while (true) {
            if (sprite.GetComponent<SpriteRenderer>().color.a == 1 || sprite.GetComponent<SpriteRenderer>().color.a < 0.2f) {
                grad *= -1;
            }
            Color c = sprite.GetComponent<SpriteRenderer>().color;
            sprite.GetComponent<SpriteRenderer>().color = new Color( c.r, c.g, c.b, c.a + grad);
            yield return null;
        }
        
    }

    public void destroyShield (GameObject player) {
        StopCoroutine(glowingShield(player));
        enableShield(player, false);
    }

    private void enableShield (GameObject player, bool enable) {
        for (int i = 0; i < player.transform.childCount; ++i) {
            if (player.transform.GetChild(i).name == "Shield") {
                // disable shield
                player.transform.GetChild(i).gameObject.SetActive(enable);
            }
        }
    }

}