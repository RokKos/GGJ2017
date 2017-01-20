//Author: Rok Kos <kosrok97@gmail.com>
//File: SpawningEnemies.cs
//File path: /D/Documents/Unity/GGJ2017/SpawningEnemies.cs
//Date: 20.01.2017
//Description: Script that spawns enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemies : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;  // GameObject for enemie
    private GameObject[] allEnemies;  // List of all enemies
    private Vector3[,] positionsFromTo;
    private const int MAXENEMIESONSCENE = 100;
	
	void Start () {
        allEnemies = new GameObject[MAXENEMIESONSCENE];
        positionsFromTo = new Vector3[MAXENEMIESONSCENE, 2];
        for (int i = 0; i < 10; ++i) {
            createEnemy();
        }
	}
	

	void Update () {
        // Moving enemies
        float speed = 1.0f * Time.deltaTime;
        for (int i = 0; i < 10; ++i) {
            allEnemies[i].transform.position = Vector3.MoveTowards(allEnemies[i].transform.position, positionsFromTo[i, 1], speed);
        }
	}

    private void createEnemy () {
        // Seeting of object
        float scaleOfEnemy = 1.0f;
        int whichSite = Random.Range(1,5);
        Vector3 startPos;
        Vector3 endPos;
        switch (whichSite) {
            case 1:
                startPos = new Vector3(10.0f, Random.Range(-10.0f, 10.0f), 0.0f);
                endPos = new Vector3(-10.0f, Random.Range(-10.0f, 10.0f), 0.0f);
                break;
            case 2:
                startPos = new Vector3(Random.Range(-10.0f, 10.0f), 10.0f, 0.0f);
                endPos = new Vector3(Random.Range(-10.0f, 10.0f), -10.0f, 0.0f);
                break;

            case 3:
                startPos = new Vector3(Random.Range(-10.0f, 10.0f), -10.0f, 0.0f);
                endPos = new Vector3(Random.Range(-10.0f, 10.0f), 10.0f, 0.0f);
                break;

            case 4:
                startPos = new Vector3(-10.0f, Random.Range(-10.0f, 10.0f), 0.0f);
                endPos = new Vector3(10.0f, Random.Range(-10.0f, 10.0f), 0.0f);
                break;

            default:
                startPos = new Vector3(10.0f, Random.Range(-10.0f, 10.0f), 0.0f);
                endPos = new Vector3(-10.0f, Random.Range(-10.0f, 10.0f), 0.0f);
                break;
        }

        Vector3 orientationOfEnemy = Vector3.RotateTowards(startPos, endPos, 0.0f, 0.0f);

        // Spawning on scene
        GameObject result = (GameObject)Instantiate(enemyPrefab, startPos, Quaternion.Euler(orientationOfEnemy));
        result.transform.localScale = new Vector3(scaleOfEnemy, scaleOfEnemy, 1);  // Setting scale of star
        int index = firstEmptyPosition();
        result.name = "Enemy" + index.ToString();
        positionsFromTo[index, 0] = startPos;
        positionsFromTo[index, 1] = endPos;
        allEnemies[index] = result;

    }

    private int firstEmptyPosition () {
        for (int i = 0; i < allEnemies.Length; ++i) {
            if (allEnemies[i] == null) {
                return i;
            }
        }
        return 0;
    }
}
