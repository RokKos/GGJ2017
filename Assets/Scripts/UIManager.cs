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
    [SerializeField] Text passedTimeText;
    [SerializeField] Text scoreEndText;
    [SerializeField] Text highScoreText;
    [SerializeField] Text scoreRealTimeText;
    [SerializeField] Text waveText;
    [SerializeField] Text waveCounterText;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject endGameMenu;
    [SerializeField] SpawningEnemies spawningEnemies;
    [SerializeField] Movement1 movement;
    [SerializeField] GameObject mainCamera;
    [SerializeField] AudioClip introSound;
    [SerializeField] AudioClip waweClearedSound;
    [SerializeField] AudioClip nearMissSound;
    private AudioSource audioSource;
    private int highscore;

    private void Start () {
        passedTimeText.text = "Time: " + 0;
        HUD.SetActive(true);
        endGameMenu.SetActive(false);
        audioSource = mainCamera.GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = introSound;
        audioSource.volume = 0.1f;
        waveText.text = "";
        waveCounterText.text = "Wave 1";

        highscore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highscore.ToString();
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

        scoreEndText.text = "Your Score: " + score.ToString();
        HUD.SetActive(false);
        endGameMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void RestartGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        audioSource.volume = 1f;
        audioSource.clip = waweClearedSound;
        audioSource.Play();
        for (int i = 0; i < 200; ++i) {
            yield return null;
        }
        audioSource.volume = 0.1f;
        waveText.text = "";
        yield return null;
    }

    public IEnumerator showNearMiss () {
        waveText.text = "Good DODGE";
        audioSource.volume = 0.2f;
        audioSource.clip = nearMissSound;
        audioSource.Play();
        for (int i = 0; i < 200; ++i) {
            yield return null;
        }
        audioSource.volume = 0.1f;
        waveText.text = "";
        yield return null;
    }



}
