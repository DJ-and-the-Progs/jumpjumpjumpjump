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

    protected ScoreCounter scoreCounter;

    [SerializeField]
    protected Animator animator;

    private bool shrinking = false;


    [SerializeField]
    private SkinnedMeshRenderer skin;
    [SerializeField]
    private Color[] colors;



	// Use this for initialization
	void Start () {
        if (skin && hits < colors.Length)
        {
            skin.materials[4].color = colors[hits];
            skin.materials[5].color = colors[hits];
        }

        scoreCounter = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreCounter>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (shrinking)
        {
            this.transform.localScale *= 0.85f;
        }
	}

    protected virtual void Die()
    {
        Destroy(this.GetComponent<Collider>()); 
        if (!this.animator) Destroy(this.gameObject);
    }

    protected virtual void OnPlayerCollideWith(PlayerMovement player)
    {
        if(player.IsGoingDown())
            player.Bounce();

        this.hits--;
        if (this.hits <= 0)
        {
            if (scoreCounter)
            {
                scoreCounter.NotifyLastHit(this.transform.position + Vector3.up * 0.8f);
                scoreCounter.AddScore(this.killScore);
            }
            if (this.animator)
            {
                this.animator.SetTrigger("killed");
                this.shrinking = true;
            }
            this.Die();
        }
        else
        {
            if (scoreCounter)
            {
                scoreCounter.NotifyDamage(this.transform.position + Vector3.up * 0.8f);
                scoreCounter.AddScore(this.hitScore);
            }
            if (this.animator) this.animator.SetTrigger("hurt");
        }

        GameObject shockWave = Instantiate(particleEffect);
        shockWave.transform.position = this.transform.position;

        // Change color of tv
        if (skin && hits < colors.Length)
        {
            skin.materials[4].color = colors[hits];
            skin.materials[5].color = colors[hits];
        }

		AudioSource.PlayClipAtPoint(this.audioClip, this.transform.position, scoreCounter.GetVolume());
	}
}
