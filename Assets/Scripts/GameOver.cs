using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    private bool revealed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (revealed && Input.GetAxisRaw("Jump") > 0) OnRestartButton();
	}

    public void Reveal()
    {
        this.transform.FindChild("ActiveContainer").gameObject.SetActive(true);
        revealed = true;
    }
    
    public void OnRestartButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OnQuitButton()
    {
        Application.LoadLevel("MainNew");
    }
}
