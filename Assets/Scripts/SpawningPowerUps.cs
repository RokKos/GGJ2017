//Author: Rok Kos <kosrok97@gmail.com>
//File: ShieldPowerUp.cs
//File path: /D/Documents/Unity/GGJ2017/ShieldPowerUp.cs
//Date: 03.02.2017
//Description: Controls spawning power ups

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPowerUps : MonoBehaviour {

    private BasePowerUp[] allPowerUps;
    [SerializeField] GameObject prefabPowerUp;

	// Use this for initialization
	void Start () {
        allPowerUps = new BasePowerUp[2];
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space")) {
            GameObject temp = (GameObject)Instantiate(prefabPowerUp, new Vector3(0, 0, 0), Quaternion.identity);

            ShieldPowerUp tPowerUp = temp.AddComponent<ShieldPowerUp>() as ShieldPowerUp;
            allPowerUps[0] = tPowerUp;

            temp.transform.position = tPowerUp.placePowerUp();
        }
		
	}

    /// <summary>
    /// 1 : Shield Power up
    /// 2 : Explosion Power up
    /// 3 : Big enemies Power up
    /// 4 : Mirror click Power up
    /// </summary>
    /// <returns></returns>

    private int decisionOfTypePowerUp () {
        int rand = Random.Range(0, 100);
        if (rand < 60) {
            if (rand < 40) {
                return 1;
            }
            return 2;

        } else {
            if (rand < 75) {
                return 3;
            }
            return 4;
        }

    }
}
