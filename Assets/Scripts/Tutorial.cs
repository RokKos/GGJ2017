//Author: Rok Kos <kosrok97@gmail.com>
//File: Turorial.cs
//File path: /D/Documents/Unity/GGJ2017/Turorial.cs
//Date: 23.01.2017
//Description: Tutorial script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour {

    private const int FramesPerSecond = 60;  // TODO: Get real frames per second
    [SerializeField] GameObject tutorialText;
    [SerializeField] GameObject nextText;
    [SerializeField] Movement1 movement;
    [SerializeField] GameObject clickPosition;
    public GameObject[] checkpoints;
    public int stageOfTutorial = -1;
    private EnemyBaseClass tutorialEnemyData;
    private GameObject tutorialEnemy;
    [SerializeField] GameObject enemyPrefab;
    private float SIZEOFBOX_X = 7.0f;
    private float SIZEOFBOX_Y = 7.0f;
    private Vector3 center = new Vector3 (0, 0, 0);
    private string[] tutorialMessage = new string[] {
        "Welcome Commander. Crew of spaceship Bajus Zanikus reporting for duty. TAP to pinpoint location where you want to go.",
        "Wow! That was fast! If you noticed your ship manipulates time. When you move time moves.",
        "Your commands are OBEYED until the very last END. You cannot change direction until the last command is finished. There is no room for COWARDS!",
        "Time to shine Commander. DODGE incoming enemies if you don't want to MEET YOUR MAKER.",
        "NOW is time for the real deal. Watch your six.",
        "Nice manuver. They had tracking system. But watch out for next ones with BLASTER CANNONS!",
        "Ship is in good hands. Try to survive as many waves as you can. TIP: you will get better score if you travel long distances and click fewer times. Good luck commander."

    };

    

    private void Start () {
        tutorialText.SetActive(false);
        movement.gameRunning = false;
        movement.TutorialMode = true;
        Time.timeScale = 0.0f;
        stageOfTutorial = -1;
        // Geting size of screen
        Camera camera = FindObjectOfType<Camera>();
        Vector3 screenPoint1 = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        SIZEOFBOX_X = screenPoint1.x + 1;
        SIZEOFBOX_Y = screenPoint1.y + 1;
        //checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        for (int i = 0; i < checkpoints.Length; ++i) {
            checkpoints[i].SetActive(false);
        }

    }

    private void Update () {
        

        if (stageOfTutorial == -1) {
            //StartCoroutine(showTextToForDuration("Hi Commander. Crew of spaceship Bajus Zanikus reporting to your duty."));
            stageOfTutorial++;
        }

        if (stageOfTutorial == 0) {
            StartCoroutine(showTextToForDuration(tutorialMessage[0]));
            stageOfTutorial++;
            checkpoints[0].SetActive(true);
        }
        if (stageOfTutorial >= 5 && stageOfTutorial < 8) {
            float speed = Random.Range(1.5f, 2f) * Time.deltaTime;
            tutorialEnemyData.nextMove(tutorialEnemy.transform, speed);

            if ((Mathf.Abs(tutorialEnemy.transform.position.x - tutorialEnemyData.getEndPos().x) < 0.2f &&
                Mathf.Abs(tutorialEnemy.transform.position.x) >= Mathf.Abs(tutorialEnemyData.getEndPos().x) ||
                Mathf.Abs(tutorialEnemy.transform.position.y - tutorialEnemyData.getEndPos().y) < 0.2f) &&
                Mathf.Abs(tutorialEnemy.transform.position.y) >= Mathf.Abs(tutorialEnemyData.getEndPos().y) ||  // for enemies type 1
               (Mathf.Abs(tutorialEnemy.transform.position.x - center.x) >= (2 * SIZEOFBOX_Y + 1.0f) ||
               Mathf.Abs(tutorialEnemy.transform.position.y - center.y) >= (2 * SIZEOFBOX_Y + 1.0f)) ) { //  fro enemies typ 2 and 3
                
                stageOfTutorial++;

                showInstrucitons();
            }

            if (tutorialEnemyData.getType() == 3) { 

                ((EnemyShootClass)tutorialEnemyData).fireGun();
            }

        }
    }

    private IEnumerator showTextToForDuration (string text) {
        //Stop time so that player can see waht text shows
        movement.gameRunning = false;
        Time.timeScale = 0.0f;
        tutorialText.SetActive(true);
        clickPosition.SetActive(false);
        // Write out text
        
        int pos = 0;
        const int maxCharsInLine = 35;
        tutorialText.GetComponentInChildren<Text>().text = " ";
        nextText.SetActive(false);

        string[] sentence = text.Split('.');
        foreach (string part in sentence) {

            string[] words = part.Split(' ');

            foreach (string word in words) {
                int currLengt = word.Length + 1;
                if (currLengt + pos > maxCharsInLine) {
                    nextText.SetActive(true);
                    // While player doest tap on screen show message
                    while (!Input.GetMouseButtonDown(0)) {
                        yield return null;
                    }
                    nextText.SetActive(false);
                    pos = word.Length;
                    //tutorialText.GetComponentInChildren<Text>().text = word + " ";
                    tutorialText.GetComponentInChildren<Text>().text = "";
                } else {
                    pos += currLengt;
                    //tutorialText.GetComponentInChildren<Text>().text += word + " ";
                }

                // Smoth writing
                for (int i = 0; i < currLengt - 1; ++i) {
                    tutorialText.GetComponentInChildren<Text>().text += word[i];
                    for (int j = 0; j < 6; ++j) {
                        yield return null;
                    }

                }
                tutorialText.GetComponentInChildren<Text>().text += " ";

            }
            string endText = tutorialText.GetComponentInChildren<Text>().text;
            // Here is -2 because there is " " character at the end (CAUTION!!!)
            if (endText.Length > 2 && endText[endText.Length - 2] != '!' && endText[endText.Length - 2] != '?' && endText[endText.Length - 2] != ',') {
                tutorialText.GetComponentInChildren<Text>().text = endText.Substring(0, endText.Length - 1) + ".";
            }
            

            // Wait to tap for next sentence
            nextText.SetActive(true);
            while (!Input.GetMouseButtonDown(0)) {
                yield return null;
            }
            nextText.SetActive(false);
            pos = 0;
            tutorialText.GetComponentInChildren<Text>().text = "";

        }
        // End of writing
        // Before end give player option to read
        nextText.SetActive(true);
        // While player doest tap on screen show message
        while (!Input.GetMouseButtonDown(0)) {
            yield return null;
        }
        nextText.SetActive(false);

        tutorialText.SetActive(false);
        movement.gameRunning = true;
        clickPosition.SetActive(true);
        // If its end of tutorial
        if (stageOfTutorial == 8) {
            SceneManager.LoadScene("StartScene");
        }
        //Time.timeScale = movement.minTimeSpeed;

    }

    public void showInstrucitons () {
        if (stageOfTutorial == 2) {
            StartCoroutine(showTextToForDuration(tutorialMessage[1]));
        }

        if (stageOfTutorial == 4) {
            StartCoroutine(showTextToForDuration(tutorialMessage[2]));
        }

        if (stageOfTutorial == 5) {
            checkpoints[3].SetActive(false);
            StartCoroutine(showTextToForDuration(tutorialMessage[3]));
            // Spawning enemy

            Vector3 startPos = new Vector3(SIZEOFBOX_X, -SIZEOFBOX_Y, 0.0f);
            Vector3 endPos = new Vector3(-SIZEOFBOX_X, SIZEOFBOX_Y, 0.0f);
            int tip = 1;
            float colliderSize = 0.08f;
            Sprite imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy1");

            if (tutorialEnemy == null) {
                tutorialEnemy = (GameObject)Instantiate(enemyPrefab, startPos, Quaternion.identity);
            }
            //else {
            //    tutorialEnemy.transform.position = startPos;
            //}

            GameObject trail1 = tutorialEnemy.transform.FindChild("Trail1").gameObject;
            trail1.SetActive(false);
            tutorialEnemy.transform.position = startPos;
            trail1.SetActive(true);

            tutorialEnemy.GetComponent<SpriteRenderer>().sprite = imageOfEnemy;
            tutorialEnemy.GetComponent<CircleCollider2D>().radius = colliderSize;
            tutorialEnemy.name = "EnemyTutorial";

            rotateEnemy(tutorialEnemy.transform, endPos);

            tutorialEnemyData = new EnemyBaseClass((byte)tip, startPos, endPos);
            
        }

        if (stageOfTutorial == 6) {
            StartCoroutine(showTextToForDuration(tutorialMessage[4]));

            Vector3 startPos = new Vector3(SIZEOFBOX_X/2, -SIZEOFBOX_Y, 0.0f);
            Vector3 endPos = new Vector3(-SIZEOFBOX_X/2, SIZEOFBOX_Y, 0.0f);
            int tip = 2;
            float colliderSize = 0.13f;
            Sprite imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy2");

            GameObject trail1 = tutorialEnemy.transform.FindChild("Trail1").gameObject;
            trail1.SetActive(false);
            tutorialEnemy.transform.position = startPos;
            trail1.SetActive(true);

            tutorialEnemy.GetComponent<SpriteRenderer>().sprite = imageOfEnemy;
            tutorialEnemy.GetComponent<CircleCollider2D>().radius = colliderSize;
            tutorialEnemy.name = "EnemyTutorial";

            rotateEnemy(tutorialEnemy.transform, endPos);

            tutorialEnemyData = new EnemyCurveClass((byte)tip, startPos, endPos, Time.time, center,  SIZEOFBOX_X, SIZEOFBOX_Y);

        }

        if (stageOfTutorial == 7)
        {
            StartCoroutine(showTextToForDuration(tutorialMessage[5]));

            Vector3 startPos = new Vector3(SIZEOFBOX_X / 2, -SIZEOFBOX_Y, 0.0f);
            Vector3 endPos = new Vector3(-SIZEOFBOX_X / 2, SIZEOFBOX_Y, 0.0f);
            int tip = 3;
            float colliderSize = 0.14f;
            //Sprite imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy3");


            //GameObject trail1 = tutorialEnemy.transform.FindChild("Trail1").gameObject;
            //trail1.SetActive(false);
            tutorialEnemy.transform.position = startPos;
            //trail1.SetActive(true);

            //tutorialEnemy.GetComponent<SpriteRenderer>().sprite = imageOfEnemy;
            //tutorialEnemy.GetComponent<CircleCollider2D>().radius = colliderSize;
            tutorialEnemy = GameObject.Find("Enemy3");
            tutorialEnemy.name = "EnemyTutorial";

            //rotateEnemy(tutorialEnemy.transform, endPos);

            tutorialEnemyData = new EnemyShootClass((byte)tip, startPos, endPos, Time.time, center, SIZEOFBOX_X, SIZEOFBOX_Y, Random.Range(3f, 6f), Random.Range(0f, 5f));
            ((EnemyShootClass)tutorialEnemyData).setShooter(tutorialEnemy.transform);



        }

        //if (stageOfTutorial == 7)
        //{
        //    StartCoroutine(showTextToForDuration(tutorialMessage[5]));

        //    Vector3 startPos = new Vector3(SIZEOFBOX_X / 2, -SIZEOFBOX_Y, 0.0f);
        //    Vector3 endPos = new Vector3(-SIZEOFBOX_X / 2, SIZEOFBOX_Y, 0.0f);
        //    int tip = 3;
        //    float colliderSize = 0.14f;
        //    Sprite imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy3");


        //    GameObject trail1 = tutorialEnemy.transform.FindChild("Trail1").gameObject;
        //    trail1.SetActive(false);
        //    tutorialEnemy.transform.position = startPos;
        //    trail1.SetActive(true);

        //    tutorialEnemy.GetComponent<SpriteRenderer>().sprite = imageOfEnemy;
        //    tutorialEnemy.GetComponent<CircleCollider2D>().radius = colliderSize;
        //    tutorialEnemy.name = "EnemyTutorial";

        //    rotateEnemy(tutorialEnemy.transform, endPos);

        //    tutorialEnemyData = new EnemyShootClass((byte)tip, startPos, endPos, Time.time, center, SIZEOFBOX_X, SIZEOFBOX_Y, Random.Range(3f, 6f), Random.Range(0f, 5f));
        //    ((EnemyShootClass)tutorialEnemyData).setShooter(tutorialEnemy.transform);



        //}

        if (stageOfTutorial == 8) {
            
            StartCoroutine(showTextToForDuration(tutorialMessage[6]));
            
        }

    }

    private void rotateEnemy (Transform enemy, Vector3 endPosition) {
        Vector3 newDir = endPosition - enemy.transform.position;
        float angle = Vector3.Angle(newDir, transform.up);

        if (Vector3.Cross(newDir, transform.up).z < 0)
            transform.Rotate(Vector3.forward, angle);
        else
            transform.Rotate(Vector3.forward, -angle);

    }
}
