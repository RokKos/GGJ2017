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
    [SerializeField] Text scoreTimeText;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject endGameMenu;
    [SerializeField] SpawningEnemies spawningEnemies;
    [SerializeField] Movement1 movement;

    private void Start () {
        passedTimeText.text = "Time: " + 0;
        HUD.SetActive(true);
        endGameMenu.SetActive(false);
    }

    private void Update () {
        passedTimeText.text = "Time: " + spawningEnemies.timePassed.ToString();
    }

    public void endGame (float score) {
        scoreTimeText.text = "Your Score: " + score.ToString();
        HUD.SetActive(false);
        endGameMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void RestartGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




}
