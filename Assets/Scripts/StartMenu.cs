using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credistsMenu;

    private void Start () {
        // Set Main Page active
        onBackClicked();
    }

    public void onStartClicked()
    {
        Debug.Log("START");
        SceneManager.LoadScene("Main");
    }

    public void onExitClicked()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void onCredistsClicked () {
        mainMenu.SetActive(false);
        credistsMenu.SetActive(true);
    }

    public void onBackClicked () {
        mainMenu.SetActive(true);
        credistsMenu.SetActive(false);
    }
}
