using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credistsMenu;
    [SerializeField] GameObject leaderBoardMenu;
    [SerializeField] GameObject usernamePanel;
    [SerializeField] InputField displayName;
    [SerializeField] GameObject addUsernameButton;
    [SerializeField] GameObject BlackOut;

    private void Start () {
        // Set Main Page active
        onBackClicked();


        //PlayerPrefs.DeleteAll();
    }

    public void onStartClicked()
    {
        //Debug.Log("START");
        if (PlayerPrefs.GetString("NameOfPlayer", "") == "") {
            mainMenu.SetActive(false);
            credistsMenu.SetActive(false);
            usernamePanel.SetActive(true);
            BlackOut.SetActive(false);
            leaderBoardMenu.SetActive(false);
        } else {
            LoadMainSceneAsync();
        }
    }

    public void onExitClicked()
    {
        //Debug.Log("EXIT");
        Application.Quit();
    }

    public void onCredistsClicked () {
        mainMenu.SetActive(false);
        credistsMenu.SetActive(true);
        usernamePanel.SetActive(false);
        BlackOut.SetActive(false);
        leaderBoardMenu.SetActive(false);
    }


    public void onTutorialClicked () {
        //Debug.Log("START");
        SceneManager.LoadScene("MainTutorial");
    }

    public void onOKClicked()
    {
        string username = displayName.text;

        if (username != "")
            PlayerPrefs.SetString("NameOfPlayer", username);

        onStartClicked();
    }

    public void onSkipClicked () {
        LoadMainSceneAsync();
    }

    private void LoadMainSceneAsync () {
        SceneManager.LoadSceneAsync("Main");
        StartCoroutine(FadeOut());

    }

    private IEnumerator FadeOut () {
        BlackOut.SetActive(true);
        float alpha = 0.0f;
        while (alpha < 1.0f) {
            Color newColor = BlackOut.GetComponent<Image>().color;
            BlackOut.GetComponent<Image>().color =new Color(newColor.r, newColor.g, newColor.b, alpha);
            alpha += 0.01f;
            yield return null;
        }

        yield return null;
    }

    public void onBackClicked () {
        mainMenu.SetActive(true);
        credistsMenu.SetActive(false);
        usernamePanel.SetActive(false);
        BlackOut.SetActive(false);
        leaderBoardMenu.SetActive(false);
    }

    public void onLeaderBoardsClicked () {
        mainMenu.SetActive(false);
        credistsMenu.SetActive(false);
        usernamePanel.SetActive(false);
        BlackOut.SetActive(false);
        leaderBoardMenu.SetActive(true);
    }
}
