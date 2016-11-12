using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Buttons called by the buttons themselves

    public void OnPlayButtonClicked()
    {
        Application.LoadLevel("game");
    }

    public void OnSettingsButtonClicked()
    {
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
