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
    [SerializeField] AudioClip[] TalksSound;
    [SerializeField] AudioSource audioSource;
    public GameObject[] checkpoints;
    public int stageOfTutorial = -1;
    private EnemyBaseClass tutorialEnemyData;
    private GameObject tutorialEnemy;
    [SerializeField] GameObject enemyPrefab;
    private float SIZEOFBOX_X;
    private float SIZEOFBOX_Y;
    private Vector3 center = new Vector3 (0, 0, 0);
    private bool waitToSpeak = false;

    private string[] tutorialMessage = new string[] {
        "Welcome Commander. The crew is reporting for duty. To move the ship simply TAP the screen. Try moving the ship through all the checkpoints.", //The ship will automatically move to that position.
        "Good JOB! If you noticed your ship MANIPULATES TIME. When traveling, time speeds up and when the designated position is reached, time will slow down.",
        "But be careful! We CANNOT CHANGE course until we reach the GREEN circle.",
        "Our scanners are picking up several enemy ships. We will have to DODGE the enemies if we don't want to MEET OUR MAKER! Get ready Commander!",
        "Good job Commander. We survived the first wave. But it's only going to get harder. The next ship has a TRACKING SYSTEM and will follow us. But it has a weak spot. It cannot track us if we get BEHIND it. Watch your six!",
        "Nice maneuver. Get ready for the final challenge. The next ship has a tracking system and a BLASTER CANNON!",
        "Outstanding performance Commander! We survived all the waves. Ship is in good hands. TIP: you will get better score if you travel longer distances and click fewer times. Good luck Commander."

    };

    private void Start () {
        tutorialText.SetActive(false);
        movement.gameRunning = false;
        movement.TutorialMode = true;
        waitToSpeak = false;
        Time.timeScale = 0.0f;
        stageOfTutorial = -1;
        // Geting size of screen
        Camera camera = FindObjectOfType<Camera>();
        Vector3 screenPoint1 = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        SIZEOFBOX_X = screenPoint1.x + 0.5f;
        SIZEOFBOX_Y = screenPoint1.y + 0.5f;
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
        yield return null;  // This is here to reset GetMouseButtonDown by waiting one frame

        //Stop time so that player can see waht text shows
        movement.gameRunning = false;
        Time.timeScale = 0.0f;
        // Stop playing current sound
        movement.audioSource.Stop();
        tutorialText.SetActive(true);
        clickPosition.SetActive(false);
        // Write out text
        
        int pos = 0;
        const int maxCharsInLine = 70;
        tutorialText.GetComponentInChildren<Text>().text = "";
        nextText.SetActive(false);

        string[] sentence = text.Split('.');
        string[] blocks = new string[sentence.Length + text.Length / maxCharsInLine];  // It will be maximal of this lengt
        int indexOfBlock = 0;
        foreach (string part in sentence) {
            // Check if there is no empty strings
            if (part.Length == 0) {
                continue;
            }
            int currLengt = part.Length;

            if (currLengt + pos > maxCharsInLine) {
                indexOfBlock++;
                blocks[indexOfBlock] = part;
                pos = currLengt;
                if (part.Length > 1 && part[part.Length - 1] != '!' && part[part.Length - 1] != '?' && part[part.Length - 1] != ',' && part[part.Length - 1] != '.') {
                    blocks[indexOfBlock] += ".";
                    pos++;
                }
                continue;
            }
            pos += currLengt;
            blocks[indexOfBlock] += part;
            if (part.Length > 1 && part[part.Length - 1] != '!' && part[part.Length - 1] != '?' && part[part.Length - 1] != ',' && part[part.Length - 1] != '.') {
                blocks[indexOfBlock] += ".";
                pos++;
            }
        }

        foreach (string block in blocks) {
            if (block == null) {
                continue;
            } 
            int i = 0;

            
            audioSource.volume = 0.3f;
            playRandomTalk();
            // Does normal writing if player doesnt clics if it does then just write whole thing
            for(i  = 0; i < block.Length; ++i) {
                tutorialText.GetComponentInChildren<Text>().text += block[i];
                for (int j = 0; j < 6; ++j) {
                    if (Input.GetMouseButtonDown(0)) {
                        break;
                    }
                    yield return null;
                }

                if (!audioSource.isPlaying && !waitToSpeak) {
                    playRandomTalk();
                    StartCoroutine(pauseBeetweenTalk(1));
                }


            }

            audioSource.Stop();
            audioSource.volume = 0.1f;
            // Write whole thing
            if (i < block.Length) {
                tutorialText.GetComponentInChildren<Text>().text = block;
            }

            yield return null;  // This is here to reset GetMouseButtonDown (so that player doesnt miss click twice)

            nextText.SetActive(true);
            // While player doest tap on screen show message 
            // This basicly wait for player to read
            while (!Input.GetMouseButtonDown(0)) {
                yield return null;
            }
            nextText.SetActive(false);

            tutorialText.GetComponentInChildren<Text>().text = "";
            yield return null;  // This is here to reset GetMouseButtonDown

        }
        // End of writing

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

            //rotateEnemy(tutorialEnemy.transform, endPos);
            tutorialEnemy.transform.rotation = new Quaternion();

            tutorialEnemyData = new EnemyCurveClass((byte)tip, startPos, endPos, Time.time, center,  SIZEOFBOX_X, SIZEOFBOX_Y);

        }

        if (stageOfTutorial == 7)
        {
            StartCoroutine(showTextToForDuration(tutorialMessage[5]));

            Vector3 startPos = new Vector3(SIZEOFBOX_X / 2, -SIZEOFBOX_Y, 0.0f);
            Vector3 endPos = new Vector3(-SIZEOFBOX_X / 2, SIZEOFBOX_Y, 0.0f);
            int tip = 3;
            float colliderSize = 0.14f;
            Sprite imageOfEnemy = (Sprite)Resources.Load<Sprite>("enemy3");


            GameObject trail1 = tutorialEnemy.transform.FindChild("Trail1").gameObject;
            trail1.SetActive(false);
            tutorialEnemy.transform.position = startPos;
            trail1.SetActive(true);

            tutorialEnemy.GetComponent<SpriteRenderer>().sprite = imageOfEnemy;
            tutorialEnemy.GetComponent<CircleCollider2D>().radius = colliderSize;
            tutorialEnemy.name = "EnemyTutorial";

            //rotateEnemy(tutorialEnemy.transform, GameObject.Find("Player").transform.position);
            tutorialEnemy.transform.rotation = new Quaternion();

            tutorialEnemyData = new EnemyShootClass((byte)tip, startPos, endPos, Time.time, center, SIZEOFBOX_X, SIZEOFBOX_Y, Random.Range(3f, 6f), Random.Range(0f, 5f));
            ((EnemyShootClass)tutorialEnemyData).setShooter(tutorialEnemy.transform);



        }

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

    private void playRandomTalk () {
        int chooseClip = Random.Range(0, TalksSound.Length);
        //float choosePitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = TalksSound[chooseClip];
        //audioSource.pitch = choosePitch;
        audioSource.Play();
    }

    private IEnumerator pauseBeetweenTalk (int seconds) {
        const int framesPerSecond = 60;
        int count = 0;
        waitToSpeak = true;
        while (count < framesPerSecond * seconds) {
            count++;
            yield return null;
        }

        waitToSpeak = false;
        
    }
}
