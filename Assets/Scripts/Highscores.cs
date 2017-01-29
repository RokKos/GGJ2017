using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour{

    private const string privateCode = "EVWGpuw5H0OMgVlswN3riwWmy_QQEUi0OT1b7KRCGJcw";
    private const string publicCode = "5888c705b6dd1500a4e3927f";
    private const string URL = "http://dreamlo.com/lb/";

    //public GameObject addScorePanel;
    //public GameObject noConnectionPanel;
    public Text UploadMessageText; 
    //public Text highscoresList;

    [SerializeField] UIManager uiManager;  
    [SerializeField] GameObject contentPanel;  // Where button will go
    [SerializeField] GameObject leaderboardColumPrefab;  // Prefab to instantiate
    private GameObject[] leaderBoardsColums;  // Just to delete them easily
    private int numberOfPlayers = 100;
    private int[] scoresToBeat;  // Scores that are top and must player beat to be on leadeboards
    private bool internetConnection;
    private bool uploaded;
    private string username;

    void Awake()
    {
        numberOfPlayers = 100;
        leaderBoardsColums = new GameObject[numberOfPlayers];
        scoresToBeat = new int[numberOfPlayers];
        updateHighscores();
        uploaded = false;
    }

    void Start()
    {
        username = PlayerPrefs.GetString("NameOfPlayer", "");
    }

    void Update()
    {
        checkInternetConnection();
    }

    //public void checkInternetConnection()
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)
    //    {
    //        if (internetConnection == true)
    //        {
    //            noConnectionPanel.SetActive(true);
    //            addScorePanel.SetActive(false);
    //            internetConnection = false;
    //        }
            
    //    }
    //    else
    //    {
    //        if (internetConnection == false)
    //        {
    //            noConnectionPanel.SetActive(false);
    //            addScorePanel.SetActive(true);
    //            internetConnection = true;
    //        }
    //    }

    //    if (uploaded)
    //    {
    //        addScorePanel.SetActive(false);
    //        noConnectionPanel.SetActive(false);
    //    }
    //}

    public void checkInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //UploadMessageText.text = "No internet connection.";
            internetConnection = false;
        }
        else
        {
            if (!uploaded)
            {
                UploadMessageText.text = "Uploading Score";
            }
            internetConnection = true;
        }



        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    if (internetConnection == true)
        //    {
        //        noConnectionPanel.SetActive(true);
        //        addScorePanel.SetActive(false);
        //        internetConnection = false;
        //    }

        //}
        //else
        //{
        //    if (internetConnection == false)
        //    {
        //        noConnectionPanel.SetActive(false);
        //        addScorePanel.SetActive(true);
        //        internetConnection = true;
        //    }
        //}

        //if (uploaded)
        //{
        //    addScorePanel.SetActive(false);
        //    noConnectionPanel.SetActive(false);
        //}
    }

    public void setUploaded(bool state)
    {
        uploaded = state;
    }

    public void addNewHighscore()
    {
        if (!uploaded){
            // Save player name for later use
            //PlayerPrefs.SetString("NameOfPlayer", username);

            // WARNING This only works if player ended game!!!
            int score = uiManager.endGameScore;
            if (username.Length > 0) {
                StartCoroutine(uploadNewHighscore(username, score));
            } else {
                StartCoroutine(uploadNewHighscore("EndoplazmatskiRetikulum", score));
                //Debug.Log("Missing username...");   //načeloma to ni potrebno ker če je username prazen se ne posodobi na strani
            }
        }            
    }

    private IEnumerator uploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(URL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            //Debug.Log("Upload Successful!");
            UploadMessageText.text = "Upload successful!";
            uploaded = true;
            //hideUploadPanel();
        }
        else {
            //Debug.Log("Upload Error: " + www.error);
            UploadMessageText.text = "Upload failed";
            uploaded = false;
        }

        //updateHighscores();
    }

    public void updateHighscores()
    {
        StartCoroutine(downloadHighscore(numberOfPlayers));
    }

    private IEnumerator downloadHighscore(int topNplayers)
    {
        WWW www = new WWW(URL + publicCode + "/pipe/" + topNplayers.ToString());  // get only top N players seperated with |
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            //Debug.Log("Download Successful!");
            //highscoresList.text = "Highscores\n\n" + parseHighscores(www.text);
            parseHighscores(www.text);
        }
        else {
            //Debug.Log("Download Error: " + www.error);
        }
    }

    private void parseHighscores(string text)
    {
        // Parse Data
        string[] splitScores = text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        //string formatedScores = "";
        string[] splitLine;
        
        //Reset before creating new colums
        DestroyAllColums();

        for (int i = 0; i < splitScores.Length; i++)
        {
            splitLine = splitScores[i].Split('|');
            // Create objects
            GameObject tempColum = (GameObject)Instantiate(leaderboardColumPrefab);
            tempColum.transform.SetParent(contentPanel.transform);
            tempColum.transform.Find("Place").GetComponent<Text>().text = (i + 1).ToString();
            tempColum.transform.Find("Name").GetComponent<Text>().text = splitLine[0].Replace('+', ' ');
            tempColum.transform.Find("Score").GetComponent<Text>().text = splitLine[1];
            tempColum.transform.localScale = new Vector3(1f, 1f, 1f);
            leaderBoardsColums[i] = tempColum;

            // Saves score to scoreToBeat
            int value;
            // Try to parse
            if (Int32.TryParse(splitLine[1], out value)) {
                scoresToBeat[i] = value;
            } else {
                //Debug.Log("String could not be parsed.");
            }
            //formatedScores += splitLine[0].Replace('+', ' ') + ":\t" + splitLine[1] + "\n";
        }

        //return formatedScores;
    }


    private void DestroyAllColums () {
        for (int i = 0; i < leaderBoardsColums.Length; ++i) {
            if (leaderBoardsColums[i] != null) {
                Destroy(leaderBoardsColums[i]);
                leaderBoardsColums[i] = null;
            }
        }
    }

    public int positionOnLeaderBoard (int score) {
        int pos = 0;
        for (int i = numberOfPlayers - 1; i >= 0; --i) {
            if (scoresToBeat[i] > score) {
                pos = i;
                break;
            }
        }
        return pos + 1;  // Because when you beat plyer you take his plce
    }
}
