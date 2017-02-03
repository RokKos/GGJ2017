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
        GameObject temp = (GameObject)Instantiate(prefabPowerUp, new Vector3(0, 0, 0), Quaternion.identity);
        
        ShieldPowerUp tPowerUp = temp.AddComponent<ShieldPowerUp>() as ShieldPowerUp;
        allPowerUps[0] = tPowerUp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
