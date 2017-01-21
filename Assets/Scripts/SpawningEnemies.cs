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
    [SerializeField] UIManager uiManager;
    private GameObject[] allEnemies;  // List of all enemies
    private EnemyBaseClass[] enemiesData;
    private const int MAXENEMIESONSCENE = 1000;
    private float SIZEOFBOX_X = 7.0f;
    private float SIZEOFBOX_Y = 7.0f;
    private int currNumberOfEnemies = 10;  // Current number enemies in scene
    private int enemiesDiedInWave = 0;
    private int waweNumber = 1;
    private const int spawnNewEnemyInSeconds = 3;  // When new enemy spawns
    public float timePassed = 0.0f;
    private float timeBetwenSpawn = 0.0f;
    //private const int BOUND = 20;
    private int gameScore = 0;
    private Vector3 playerPos;


    void Start () {

        Camera camera = FindObjectOfType<Camera>();
        Vector3 screenPoint1 = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        SIZEOFBOX_X = Mathf.Max(screenPoint1.x, screenPoint1.y) + 1;
        SIZEOFBOX_Y = SIZEOFBOX_X;
        //SIZEOFBOX_Y = Mathf.Max(screenPoint1.x, screenPoint1.y) + 1;

        timePassed = 0.0f;
        timeBetwenSpawn = 0.0f;
        waweNumber = 1;
        gameScore = 0;
        playerPos = new Vector3(0, 0, 0);
        currNumberOfEnemies = 10;
        enemiesDiedInWave = 0;
        enemiesData = new EnemyBaseClass[MAXENEMIESONSCENE];
        allEnemies = new GameObject[MAXENEMIESONSCENE];
        for (int i = 0; i < currNumberOfEnemies; ++i) {
            //TODO: Create advanced enemies later
            int tip = randomPick();
            createEnemy(i, tip);
        }
        //Debug.Log("ScreenPoint1: " + screenPoint1.x + " " + screenPoint1.y + " " + screenPoint1.z);
    }
	

	void Update () {
        // Moving enemies
        float speed = 2.0f * Time.deltaTime;
        for (int i = 0; i < currNumberOfEnemies; ++i) {
            enemiesData[i].nextMove(allEnemies[i].transform, speed);
            // Checking if they are at the end
            if ((Mathf.Abs(allEnemies[i].transform.position.x - playerPos.x) >= SIZEOFBOX_X ||
                Mathf.Abs(allEnemies[i].transform.position.y - playerPos.y) >= SIZEOFBOX_Y) &&
                enemiesData[i].getType() != 4 && // if is bullet do not instantiate again
                timePassed > 2.0f) {
                // Not deleting object but rather just moving it to another starting point
                //Debug.Log("Destroyed at  x: " + allEnemies[i].transform.position.x + "    y :" + allEnemies[i].transform.position.y);
                enemiesDiedInWave++;
                if (enemiesDiedInWave == currNumberOfEnemies) {
                    nextWave();
                    
                }
            }

            // Check if they can shoot
            if (enemiesData[i].getType() == 3) {
                /*GameObject tempBullet = (GameObject)Instantiate(bulletPrefab, allEnemies[i].transform.position,
                    Quaternion.Euler(Quaternion.ToEulerAngles(allEnemies[i].transform.rotation)));
                    
                createEnemy(currNumberOfEnemies, 4);
                currNumberOfEnemies++;
                Destroy(tempBullet, 10);*/

                ((EnemyShootClass)enemiesData[i]).fireGun();
            }
             
        }

        // Check if you can spawn new enemy
        /*if (timeBetwenSpawn > spawnNewEnemyInSeconds) {
            timeBetwenSpawn = 0.0f;
            // First create enemy with that number and then add because for loop goes to one less than currNumberOfEnmies
            int tip = randomPick();
            createEnemy(currNumberOfEnemies, tip);
            currNumberOfEnemies++;
        }

        // Check if dificulty gets tuffer
        // Tweak a little this parameters

        if (currNumberOfEnemies > stageNumber * BOUND && currNumberOfEnemies < (stageNumber + 1) * BOUND) {
            createWaveOfEnemies(BOUND / 2);
            stageNumber++;
            zgornjaIzbira++;
            zgornjaIzbira = Mathf.Min(3, zgornjaIzbira);
            if (zgornjaIzbira == 3) {
                spodnjaIzbira++;
                spodnjaIzbira = Mathf.Min(3, spodnjaIzbira);
            }
        }*/
        timeBetwenSpawn += Time.deltaTime;
        timePassed += Time.deltaTime;
	}

    private void createEnemy (int index, int tip) {
        // Seeting of object
        float scaleOfEnemy = 1.0f;
        int whichSite = Random.Range(1,5);

        Vector3 startPos;
        Vector3 endPos;
        switch (whichSite) {
            case 1:
                startPos = new Vector3(SIZEOFBOX_X, Random.Range(-SIZEOFBOX_Y, SIZEOFBOX_Y), 0.0f);
                endPos = new Vector3(-SIZEOFBOX_X, Random.Range(-SIZEOFBOX_Y, SIZEOFBOX_Y), 0.0f);
                break;
            case 2:
                startPos = new Vector3(Random.Range(-SIZEOFBOX_X, SIZEOFBOX_X), SIZEOFBOX_Y, 0.0f);
                endPos = new Vector3(Random.Range(-SIZEOFBOX_X, SIZEOFBOX_X), -SIZEOFBOX_Y, 0.0f);
                break;

            case 3:
                startPos = new Vector3(Random.Range(-SIZEOFBOX_X, SIZEOFBOX_X), -SIZEOFBOX_Y, 0.0f);
                endPos = new Vector3(Random.Range(-SIZEOFBOX_X, SIZEOFBOX_X), SIZEOFBOX_Y, 0.0f);
                break;

            case 4:
                startPos = new Vector3(-SIZEOFBOX_X, Random.Range(-SIZEOFBOX_Y, SIZEOFBOX_Y), 0.0f);
                endPos = new Vector3(SIZEOFBOX_X, Random.Range(-SIZEOFBOX_Y, SIZEOFBOX_Y), 0.0f);
                break;

            default:
                startPos = new Vector3(SIZEOFBOX_X, Random.Range(-SIZEOFBOX_Y, SIZEOFBOX_Y), 0.0f);
                endPos = new Vector3(-SIZEOFBOX_X, Random.Range(-SIZEOFBOX_Y, SIZEOFBOX_Y), 0.0f);
                break;
        }

        //Vector3 orientationOfEnemy = Vector3.RotateTowards(startPos, endPos, 0.0f, 0.0f);

        // Spawning on scene
        GameObject result;
        EnemyBaseClass temp = new EnemyBaseClass((byte)tip, startPos, endPos);
        Sprite imageOfEnemy = new Sprite(); 
        if (tip == 1 || tip == 4) {
            temp = new EnemyBaseClass((byte)tip, startPos, endPos);
            imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy1");
        } else if (tip == 2) {
            temp = new EnemyCurveClass((byte)tip, startPos, endPos, Time.time, playerPos, SIZEOFBOX_X, SIZEOFBOX_Y);  // TODO: Get user position
            imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy2");
        } else if (tip == 3) {
            temp = new EnemyShootClass((byte)tip, startPos, endPos, Time.time, playerPos, SIZEOFBOX_X, SIZEOFBOX_Y, Random.Range(3f, 6f), Random.Range(0f, 5f));  // TODO: Get user position
            imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy3");
        }

        if (allEnemies[index] == null) {
            result = (GameObject)Instantiate(enemyPrefab, startPos, Quaternion.identity);
            //result.transform.rotation = Quaternion.Euler(0, 0, 90);
            rotateEnemy(result.transform);
            //result.transform.LookAt(playerPos);
            enemiesData[index] = temp;
        } else {
            result = allEnemies[index];
            GameObject trail1 = result.transform.FindChild("Trail1").gameObject;
            trail1.SetActive(false);
            result.transform.position = startPos;
            trail1.SetActive(true);
            rotateEnemy(result.transform);
            //result.transform.rotation = Quaternion.identity;
            //result.transform.rotation = Quaternion.Euler(0, 0, 90);
            //result.transform.LookAt(playerPos);
            enemiesData[index] = temp;
        }
        // Image of enemy
        result.GetComponent<SpriteRenderer>().sprite = imageOfEnemy;
        result.transform.localScale = new Vector3(scaleOfEnemy, scaleOfEnemy, 1);  // Setting scale of star
        result.name = "Enemy" + index.ToString();

        if (tip == 3)
        {
            ((EnemyShootClass)temp).setShooter(result.transform);
        }
        
        allEnemies[index] = result;
    }

    private void rotateEnemy(Transform enemy)
    {
        //Debug.Log("x: " + enemy.position.x + "  y: " + enemy.position.y + "  z: " + enemy.position.z);

        if (enemy.position.x == SIZEOFBOX_X)
            enemy.rotation = Quaternion.Euler(0, 0, 90);

        if (enemy.position.x == -SIZEOFBOX_X)
            enemy.rotation = Quaternion.Euler(0, 0, -90);

        if (enemy.position.y == SIZEOFBOX_X)
            enemy.rotation = Quaternion.Euler(0, 0, 180);

        if (enemy.position.y == -SIZEOFBOX_X)
            enemy.rotation = Quaternion.Euler(0, 0, 0);
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
        int radomNum = Random.Range(1, 101);
        if (radomNum < 100 - (waweNumber * Mathf.Sqrt(currNumberOfEnemies)))
        {
            return 1;
        }
        else if (radomNum < 100 - (waweNumber * Mathf.Pow(currNumberOfEnemies, 1.0f / 3)))
        {
            return 2;
        }
        return 3;
    }

    private void createWaveOfEnemies (int number) {
        for (int i = 0; i < number; ++i) {
            int tip = randomPick();
            createEnemy(i, tip);
            //currNumberOfEnemies++;
        }
    }

    private void nextWave () {
        currNumberOfEnemies += 2;
        enemiesDiedInWave = 0;
        createWaveOfEnemies(currNumberOfEnemies);
        uiManager.updateScoreText(calculateScore());

        StartCoroutine(uiManager.showWaveCleared(waweNumber));

    }

    public int calculateScore () {
        int result = 0;

        result = waweNumber *  currNumberOfEnemies;
        gameScore += result;
        return gameScore;
    }

    
}
