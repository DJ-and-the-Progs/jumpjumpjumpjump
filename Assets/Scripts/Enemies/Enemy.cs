using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private AudioClip audioClip;

	[SerializeField]
    private GameObject particleEffect;

    [SerializeField]
    private int hits = 1;

	[SerializeField]
    [Tooltip("How namy points recieved for doing damage")]
    private int hitScore;
    [SerializeField]
    [Tooltip("How many points received for final hit")]
	private int killScore;

    private int maxHits;

    protected ScoreCounter scoreCounter;

    [SerializeField]
    protected Animator animator;


    [SerializeField]
    private SkinnedMeshRenderer skin;
    [SerializeField]
    private Color[] colors;



	// Use this for initialization
	void Start () {
        maxHits = hits;
        skin.materials[4].color = colors[hits];
        skin.materials[5].color = colors[hits];

        scoreCounter = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreCounter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected virtual void Die()
    {
        Destroy(this.GetComponent<Collider>()); 
        if (!this.animator) Destroy(this.gameObject);
    }

    protected virtual void OnPlayerBounceOn(object[] data)
    {
        GameObject player = (GameObject)data[0];
        Vector3 hitPoint = (Vector3)data[1];
        this.hits--;
        if (this.hits <= 0)
        {
            if (scoreCounter)
            {
                scoreCounter.NotifyLastHit(this.transform.position + Vector3.up * 0.8f);
                scoreCounter.AddScore(this.killScore);
            }
            if (this.animator) this.animator.SetTrigger("killed");
            this.Die();
        }
        else
        {
            if (scoreCounter)
            {
                scoreCounter.NotifyDamage(this.transform.position + Vector3.up * 0.8f);
                scoreCounter.AddScore(this.hitScore);
            }
            if (this.animator) this.animator.SetTrigger("hit");
        }

        GameObject shockWave = Instantiate(particleEffect);
        shockWave.transform.position = this.transform.position;

        // Change color of tv
        skin.materials[4].color = colors[hits];
        skin.materials[5].color = colors[hits];

		AudioSource.PlayClipAtPoint(this.audioClip, this.transform.position);
	}
}
