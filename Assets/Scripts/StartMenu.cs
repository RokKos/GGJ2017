using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

	
	public void onStartClicked()
    {
        Debug.Log("START");
        Application.LoadLevel("TestMovement1");
    }

    public void onExitClicked()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
}
