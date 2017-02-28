//Author: Rok Kos <kosrok97@gmail.com>
//File: UIManager.cs
//File path: /D/Documents/Unity/GGJ2017/UIManager.cs
//Date: 20.01.2017
//Description: Controling UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    
    [Header("UI")]
    [SerializeField] Text passedTimeText;
    [SerializeField] Text scoreEndText;
    [SerializeField] Text highScoreText;
    [SerializeField] Text scoreRealTimeText;
    [SerializeField] Text waveText;
    [SerializeField] Text waveCounterText;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject endGameMenu;
    [SerializeField] GameObject leaderboardMenu;
    [Space(5)]
    [Header("Scripts")]
    [SerializeField] SpawningEnemies spawningEnemies;
    [SerializeField] Movement1 movement;
    [SerializeField] Highscores highScores;
    [SerializeField] GameObject mainCamera;
    [Space(5)]
    [Header("Audio")]
    [SerializeField] AudioClip introSound;
    [SerializeField] AudioClip waweClearedSound;
    [SerializeField] AudioClip nearMissSound;
    [SerializeField] AudioSource audioSourceStartEnd;
    [SerializeField] AudioSource audioSourceEffects;
    private int highscore;
    public int endGameScore;

    private void Start () {
        newGame();
    }

    private void newGame()
    {
        passedTimeText.text = "Time: " + 0;
        HUD.SetActive(true);
        endGameMenu.SetActive(false);
        leaderboardMenu.SetActive(false);
        //audioSourceStartEnd = mainCamera.GetComponent<AudioSource>();
        audioSourceStartEnd.loop = false;
        audioSourceStartEnd.clip = introSound;
        audioSourceStartEnd.volume = 0.1f;
        audioSourceEffects.loop = false;
        audioSourceEffects.volume = 0.1f;
        waveText.text = "";
        waveCounterText.text = "Wave 1";
        endGameScore = 0;

        if (highScores != null)
        {
            highScores.setUploaded(false);
        }

        highscore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highscore.ToString();

        //GameObject.Find("Player").GetComponent<Movement1>().resetPlayer();

        //Debug.Log("Highscore: " + highscore);
    }

    private void Update () {
        passedTimeText.text = "Time: " + spawningEnemies.timePassed.ToString();
    }

    public void endGame (int score) {
        
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score: " + highscore.ToString();
            //Debug.Log("New Highscore: " + highscore);
        }
        // Important line for upload new highscore
        endGameScore = score;

        if (highScores != null)
        {
            //highScores.checkInternetConnection();
            highScores.addNewHighscore();
            //highScores.updateHighscores();
        }

        //if (highScores.positionOnLeaderBoard(score) == 100) {
        //    highScores.hideUploadPanel();
        //} else if (highScores.positionOnLeaderBoard(score) < 100) {
        //    highScores.showUploadPanel();
        //}


        scoreEndText.text = "Your Score: " + score.ToString();
        HUD.SetActive(false);
        endGameMenu.SetActive(true);
        leaderboardMenu.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void RestartGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //newGame();
    }

    public void GoToMenu () {
        SceneManager.LoadScene("StartScene");
    }

    public void updateScoreText (int score) {
        scoreRealTimeText.text = "Score: " + score.ToString();
        //return 0;
    }

    public IEnumerator showWaveCleared (int numberOfWave) {
        waveCounterText.text = "Wave " + (numberOfWave + 1);
        waveText.text = "WAVE " + numberOfWave.ToString() + " CLEARED!";
        audioSourceStartEnd.volume = 1f;
        audioSourceStartEnd.clip = waweClearedSound;
        audioSourceStartEnd.Play();
        while (audioSourceStartEnd.isPlaying) {
            yield return null;
        }
        audioSourceStartEnd.volume = 0.1f;
        waveText.text = "";
        yield return null;
    }

    public IEnumerator showNearMiss () {
        waveText.text = "Good DODGE";
        audioSourceEffects.volume = 0.2f;
        audioSourceEffects.clip = nearMissSound;
        audioSourceEffects.Play();
        for (int i = 0; i < 200; ++i) {
            yield return null;
        }
        audioSourceEffects.volume = 0.1f;
        waveText.text = "";
        yield return null;
    }

    public void GoToLeaderboards () {
        HUD.SetActive(false);
        endGameMenu.SetActive(false);
        leaderboardMenu.SetActive(true);
        highScores.updateHighscores();
    }

}
