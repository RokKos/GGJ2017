using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credistsMenu;
    [SerializeField] GameObject usernamePanel;
    [SerializeField] InputField displayName;
    [SerializeField] GameObject addUsernameButton;

    private void Start () {
        // Set Main Page active
        onBackClicked();
    }

    public void onStartClicked()
    {
        //Debug.Log("START");
        SceneManager.LoadScene("Main");
    }

    public void onExitClicked()
    {
        //Debug.Log("EXIT");
        Application.Quit();
    }

    public void onCredistsClicked () {
        mainMenu.SetActive(false);
        credistsMenu.SetActive(true);
    }

    public void onBackClicked () {

        if (PlayerPrefs.GetString("NameOfPlayer", "") == "")
        {
            mainMenu.SetActive(false);
            credistsMenu.SetActive(false);
            usernamePanel.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            credistsMenu.SetActive(false);
            usernamePanel.SetActive(false);
        }
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

        onBackClicked();
    }
}
