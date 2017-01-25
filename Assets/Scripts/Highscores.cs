using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour{

    private const string privateCode = "EVWGpuw5H0OMgVlswN3riwWmy_QQEUi0OT1b7KRCGJcw";
    private const string publicCode = "5888c705b6dd1500a4e3927f";
    private const string URL = "http://dreamlo.com/lb/";

    public Button upload;
    public InputField displayName;
    public GameObject addScorePanel;
    public Text highscoresList;
    
    void Awake()
    {
        updateHighscores();
    }

    void Start()
    {
        upload.onClick.AddListener(() => addNewHighscore(displayName.text, 5));
    }

    public void addNewHighscore(string username, int score)
    {
        if (username.Length > 0)
            StartCoroutine(uploadNewHighscore(username, score));
        else
            Debug.Log("Missing username...");   //načeloma to ni potrebno ker če je username prazen se ne posodobi na strani
    }

    private IEnumerator uploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(URL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            Debug.Log("Upload Successful!");
        else
            Debug.Log("Upload Error: " + www.error);

        updateHighscores();
        hideUploadPanel();
    }

    public void updateHighscores()
    {
        StartCoroutine("downloadHighscore");
    }

    private IEnumerator downloadHighscore()
    {
        WWW www = new WWW(URL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Download Successful!");
            highscoresList.text = "Highscores\n\n" + parseHighscores(www.text);
        }
        else
            Debug.Log("Download Error: " + www.error);
    }

    private string parseHighscores(string text)
    {
        string[] splitScores = text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        string formatedScores = "";
        string[] splitLine;

        for (int i = 0; i < splitScores.Length; i++)
        {
            splitLine = splitScores[i].Split('|');
            formatedScores += splitLine[0].Replace('+', ' ') + ":\t" + splitLine[1] + "\n";
        }

        return formatedScores;
    }

    public void showUploadPanel()
    {
        addScorePanel.SetActive(true);
    }

    public void hideUploadPanel()
    {
        displayName.text = "";
        addScorePanel.SetActive(false);
    }
}
