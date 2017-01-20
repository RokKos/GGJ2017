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
            createEnemy(i);
        }
	}
	

	void Update () {
        // Moving enemies
        float speed = 5.0f * Time.deltaTime;
        for (int i = 0; i < 10; ++i) {
            allEnemies[i].transform.position = Vector3.MoveTowards(allEnemies[i].transform.position, positionsFromTo[i, 1], speed);
            if (Mathf.Abs(allEnemies[i].transform.position.x - positionsFromTo[i, 1].x) < 0.1f && Mathf.Abs(allEnemies[i].transform.position.y - positionsFromTo[i, 1].y) < 0.1f) {
                // Not deleting object but rather just moving it to another starting point
                createEnemy(i);
            } 
        }
	}

    private void createEnemy (int index) {
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
        GameObject result;
        if (allEnemies[index] == null) {
            result = (GameObject)Instantiate(enemyPrefab, startPos, Quaternion.Euler(orientationOfEnemy));
        } else {
            result = allEnemies[index];
        }
         
        result.transform.localScale = new Vector3(scaleOfEnemy, scaleOfEnemy, 1);  // Setting scale of star
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
