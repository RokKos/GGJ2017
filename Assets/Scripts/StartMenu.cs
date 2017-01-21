using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	
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
}
