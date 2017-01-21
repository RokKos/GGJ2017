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
    [SerializeField] GameObject bulletPrefab;  // GameObject for enemie
    private GameObject[] allEnemies;  // List of all enemies
    private EnemyBaseClass[] enemiesData;
    private const int MAXENEMIESONSCENE = 1000;
    private const float SIZEOFBOX = 10.0f;
    private int currNumberOfEnemies = 10;  // Current number enemies in scene
    private const int spawnNewEnemyInSeconds = 3;  // When new enemy spawns
    private float timePassed = 0.0f;
    private const int BOUND = 20;
    private int spodnjaIzbira = 1;
    private int zgornjaIzbira = 2;


    void Start () {
        timePassed = 0.0f;
        spodnjaIzbira = 1;
        zgornjaIzbira = 2;
        currNumberOfEnemies = 10;
        enemiesData = new EnemyBaseClass[MAXENEMIESONSCENE];
        allEnemies = new GameObject[MAXENEMIESONSCENE];
        for (int i = 0; i < currNumberOfEnemies; ++i) {
            //TODO: Create advanced enemies later
            int tip = randomPick();
            createEnemy(i, tip);
        }
	}
	

	void Update () {
        // Moving enemies
        for (int i = 0; i < currNumberOfEnemies; ++i) {
            allEnemies[i].transform.position = enemiesData[i].nextMove(allEnemies[i].transform.position);
            // Checking if they are at the end
            if (Mathf.Abs(allEnemies[i].transform.position.x - enemiesData[i].getEndPos().x) < 0.1f &&
                Mathf.Abs(allEnemies[i].transform.position.y - enemiesData[i].getEndPos().y) < 0.1f &&
                enemiesData[i].getType() != 4) {  // if is bullet do not instantiate again
                // Not deleting object but rather just moving it to another starting point
                int tip = randomPick();
                Debug.Log(tip);
                createEnemy(i, tip);
            }

            // Check if they can shoot
            if (enemiesData[i].getType() == 3) {
                /*GameObject tempBullet = (GameObject)Instantiate(bulletPrefab, allEnemies[i].transform.position,
                    Quaternion.Euler(Quaternion.ToEulerAngles(allEnemies[i].transform.rotation)));

                createEnemy(currNumberOfEnemies, 4);
                currNumberOfEnemies++;
                Destroy(tempBullet, 10);*/
            }
             
        }

        // Check if you can spawn new enemy
        if (timePassed > spawnNewEnemyInSeconds) {
            timePassed = 0.0f;
            // First create enemy with that number and then add because for loop goes to one less than currNumberOfEnmies
            int tip = randomPick();
            createEnemy(currNumberOfEnemies, tip);
            currNumberOfEnemies++;
        }

        // Check if dificulty gets tuffer
        // Tweak a little this parameters
        if (currNumberOfEnemies > BOUND && currNumberOfEnemies < 2 * BOUND) {
            zgornjaIzbira = 3;
        } else if (currNumberOfEnemies > 2 * BOUND && currNumberOfEnemies < 3 * BOUND) {
            zgornjaIzbira = 4;
        } else if (currNumberOfEnemies > 3 * BOUND && currNumberOfEnemies < 4 * BOUND) {
            spodnjaIzbira = 2;
        } else if (currNumberOfEnemies > 4 * BOUND) {
            spodnjaIzbira = 3;
        }

        timePassed += Time.deltaTime;
	}

    private void createEnemy (int index, int tip) {
        // Seeting of object
        float scaleOfEnemy = 1.0f;
        float speed = 5.0f * Time.deltaTime;
        int whichSite = Random.Range(1,5);

        Vector3 startPos;
        Vector3 endPos;
        switch (whichSite) {
            case 1:
                startPos = new Vector3(SIZEOFBOX, Random.Range(-SIZEOFBOX, SIZEOFBOX), 0.0f);
                endPos = new Vector3(-SIZEOFBOX, Random.Range(-SIZEOFBOX, SIZEOFBOX), 0.0f);
                break;
            case 2:
                startPos = new Vector3(Random.Range(-SIZEOFBOX, SIZEOFBOX), SIZEOFBOX, 0.0f);
                endPos = new Vector3(Random.Range(-SIZEOFBOX, SIZEOFBOX), -SIZEOFBOX, 0.0f);
                break;

            case 3:
                startPos = new Vector3(Random.Range(-SIZEOFBOX, SIZEOFBOX), -SIZEOFBOX, 0.0f);
                endPos = new Vector3(Random.Range(-SIZEOFBOX, SIZEOFBOX), SIZEOFBOX, 0.0f);
                break;

            case 4:
                startPos = new Vector3(-SIZEOFBOX, Random.Range(-SIZEOFBOX, SIZEOFBOX), 0.0f);
                endPos = new Vector3(SIZEOFBOX, Random.Range(-SIZEOFBOX, SIZEOFBOX), 0.0f);
                break;

            default:
                startPos = new Vector3(SIZEOFBOX, Random.Range(-SIZEOFBOX, SIZEOFBOX), 0.0f);
                endPos = new Vector3(-SIZEOFBOX, Random.Range(-SIZEOFBOX, SIZEOFBOX), 0.0f);
                break;
        }

        Vector3 orientationOfEnemy = Vector3.RotateTowards(startPos, endPos, 0.0f, 0.0f);

        // Spawning on scene
        GameObject result;
        EnemyBaseClass temp = new EnemyBaseClass((byte)tip, startPos, endPos, speed);
        if (tip == 1 || tip == 4) {
            temp = new EnemyBaseClass((byte)tip, startPos, endPos, speed);
        } else if (tip == 2) {
            temp = new EnemyCurveClass((byte)tip, startPos, endPos, speed, Time.time, new Vector3(0,0,0));  // TODO: Get user position
        } else if (tip == 3) {
            temp = new EnemyShootClass((byte)tip, startPos, endPos, speed, Time.time, new Vector3(0,0,0), 20, Time.time);  // TODO: Get user position
        }

        if (allEnemies[index] == null) {
            result = (GameObject)Instantiate(enemyPrefab, startPos, Quaternion.Euler(orientationOfEnemy));
            enemiesData[index] = temp;
        } else {
            result = allEnemies[index];
            
            enemiesData[index] = temp;
        }
         
        result.transform.localScale = new Vector3(scaleOfEnemy, scaleOfEnemy, 1);  // Setting scale of star
        result.name = "Enemy" + index.ToString();
        
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

    private int randomPick () {
        return Random.Range(spodnjaIzbira, zgornjaIzbira);
    }
}
