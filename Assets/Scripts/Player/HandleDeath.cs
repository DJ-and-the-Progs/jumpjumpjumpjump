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
            PlayerMovement pm = GetComponent<PlayerMovement>();
            pm.Dead = true;
            Destroy(pm);
            Rigidbody rb = pm.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.velocity = Vector3.down * 19;
            
            GameObject.FindGameObjectWithTag("GameOver").SendMessage("Reveal");
        }
    }
}
