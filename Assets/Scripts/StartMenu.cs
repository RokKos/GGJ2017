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
        mainMenu.SetActive(true);
        credistsMenu.SetActive(false);
        usernamePanel.SetActive(false);


        //PlayerPrefs.DeleteAll();
    }

    public void onStartClicked()
    {
        //Debug.Log("START");
        if (PlayerPrefs.GetString("NameOfPlayer", "") == "") {
            mainMenu.SetActive(false);
            credistsMenu.SetActive(false);
            usernamePanel.SetActive(true);
        } else {
            SceneManager.LoadScene("Main");
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
        SceneManager.LoadScene("Main");
    }
}
