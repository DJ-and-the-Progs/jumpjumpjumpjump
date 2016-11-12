using UnityEngine;
using System.Collections;

public class KillPlayerOnHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPlayerBounceOn(object[] data)
    {
        GameObject player = (GameObject)data[0];
        player.SendMessage("Die");
    }

    void OnTriggerEnter(Collider col)
    {
        col.gameObject.SendMessage("Die");
    }
}
