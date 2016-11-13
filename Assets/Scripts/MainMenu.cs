using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Enter") > 0) OnPlayButtonClicked();
        if (Input.GetAxisRaw("Cancel") > 0) OnQuitButtonClicked();
    }

    // Buttons called by the buttons themselves

    public void OnPlayButtonClicked()
    {
        Application.LoadLevel("level1");
    }

    public void OnSettingsButtonClicked()
    {
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
