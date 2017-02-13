//Author: Rok Kos <kosrok97@gmail.com>
//File: ShieldPowerUp.cs
//File path: /D/Documents/Unity/GGJ2017/ShieldPowerUp.cs
//Date: 03.02.2017
//Description: Controls spawning power ups

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPowerUps : MonoBehaviour {

    public BasePowerUp[] allPowerUps;
    [SerializeField] GameObject prefabPowerUp;
    private BasePowerUp tPowerUp;
    // Use this for initialization
    void Start () {
        allPowerUps = new BasePowerUp[2];
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space")) {
            GameObject temp = (GameObject)Instantiate(prefabPowerUp, new Vector3(0, 0, 0), Quaternion.identity);

            // Just because I can leave it unsigned
            
            int which = decisionOfTypePowerUp();

            switch (which) {
                case 1:
                    tPowerUp = temp.AddComponent<ShieldPowerUp>() as ShieldPowerUp;
                    allPowerUps[0] = tPowerUp;
                    break;
                case 2:
                    // Temporary on this jsut for testing
                    tPowerUp = temp.AddComponent<ShieldPowerUp>() as ShieldPowerUp;
                    allPowerUps[0] = tPowerUp;
                    break;

                case 3:
                    tPowerUp = temp.AddComponent<BigEnemiesPowerUp>() as BigEnemiesPowerUp;
                    allPowerUps[0] = tPowerUp;
                    break;

                case 4:
                    // Temporary on this jsut for testing
                    tPowerUp = temp.AddComponent<BigEnemiesPowerUp>() as BigEnemiesPowerUp;
                    allPowerUps[0] = tPowerUp;
                    break;
            }
            

            temp.transform.position = tPowerUp.placePowerUp();
            temp.GetComponent<BasePowerUp>().loadImage();
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
