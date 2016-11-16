using UnityEngine;
using System.Collections;

public class NormalBounceObject : MonoBehaviour {

	[SerializeField]
	private AudioClip audioClip;

	[SerializeField]
    private GameObject particleEffect;


    protected ScoreCounter scoreCounter;

	// Use this for initialization
	void Start () {
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreCounter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    void OnPlayerCollideWith(PlayerMovement player)
    {
        if(player.IsGoingDown())
            player.Bounce();

        if (scoreCounter)
        {
            scoreCounter.NotifyDamage(new Vector3());
        }

        GameObject shockWave = Instantiate(particleEffect);
        shockWave.transform.position = this.transform.position;
        

		AudioSource.PlayClipAtPoint(this.audioClip, this.transform.position);
	}
}
