using UnityEngine;
using System.Collections;

public class KillPlayerOnHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPlayerBounceOn(GameObject player)
    {
        player.SendMessage("Die");
    }

    void OnTriggerEnter(Collider col)
    {
        col.gameObject.SendMessage("Die");
    }
}
