using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Die()
    {
        Destroy(this.gameObject);
    }

    void OnPlayerBounceOn(GameObject player)
    {
        this.Die();
    }
}
