//Author: Rok Kos <kosrok97@gmail.com>
//File: Turorial.cs
//File path: /D/Documents/Unity/GGJ2017/Turorial.cs
//Date: 23.01.2017
//Description: Tutorial script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    private const int FramesPerSecond = 60;  // TODO: Get real frames per second
    [SerializeField] GameObject tutorialText;
    [SerializeField] Movement1 movement;
    public GameObject[] checkpoints;
    public int stageOfTutorial = -1;

    private void Start () {
        tutorialText.SetActive(false);
        movement.gameRunning = false;
        Time.timeScale = 0.0f;
        stageOfTutorial = -1;
        checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        for (int i = 0; i < checkpoints.Length; ++i) {
            checkpoints[i].SetActive(false);
        }

        
    }

    private void Update () {
        if (stageOfTutorial == -1) {
            StartCoroutine(showTextToForDuration("Hi Commander. Crew of spaceship Bajus Zanikus reporting to your duty.", 2));
            stageOfTutorial++;
        }

        if (stageOfTutorial == 0) {
            StartCoroutine(showTextToForDuration("Click to pinpoint location where you want to go.", 2));
            stageOfTutorial++;
            checkpoints[0].SetActive(true);
        }
    }

    private IEnumerator showTextToForDuration (string text, int time) {
        int timeToFrames = time * FramesPerSecond;
        //Stop time so that player can see waht text shows
        movement.gameRunning = false;
        Time.timeScale = 0.0f;
        tutorialText.SetActive(true);
        tutorialText.GetComponentInChildren<Text>().text = text;
        for (int i = 0; i < timeToFrames; ++i) {
            yield return null;
        }
        tutorialText.SetActive(false);
        movement.gameRunning = true;
        //Time.timeScale = movement.minTimeSpeed;
        yield return null;

    }

    public void showInstrucitons () {
        if (stageOfTutorial == 2) {
            StartCoroutine(showTextToForDuration("Vau! That was fast! If you noticed your ship manipulates time. When you move time moves.", 3));
        }

        if (stageOfTutorial == 4) {
            StartCoroutine(showTextToForDuration("Your commands are OBEYED until the END. You cannot change direction until the last command is finished. There is no room for COWARDS!", 4));
        }

        if (stageOfTutorial == 5) {
            StartCoroutine(showTextToForDuration("Time to shine Commander.", 2));
        }
    }
}
