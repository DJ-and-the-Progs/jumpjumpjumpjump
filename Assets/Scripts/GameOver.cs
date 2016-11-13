using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reveal()
    {
        this.transform.FindChild("ActiveContainer").gameObject.SetActive(true);
    }
    
    public void OnRestartButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OnQuitButton()
    {
        Application.LoadLevel("main menu");
    }
}
