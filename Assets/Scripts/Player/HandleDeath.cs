using UnityEngine;
using System.Collections;

public class HandleDeath : MonoBehaviour {

    [SerializeField]
    private int hitsUntilDeath;
    private int maxHits;

	// Use this for initialization
	void Start () {
        maxHits = hitsUntilDeath;
	}
	
    // Called by triggers and KillPlayerOnHit
    void Die()
    {
        hitsUntilDeath--;
        if (hitsUntilDeath <= 0)
        {
            // GameOver
            Destroy(this.GetComponent<PlayerMovement>());
            GameObject.FindGameObjectWithTag("GameOver").SendMessage("Reveal");
        }
    }
}
