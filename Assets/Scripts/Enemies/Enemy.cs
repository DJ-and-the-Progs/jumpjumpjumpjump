﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

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

    [SerializeField]
    protected bool debugScaleOnHit = true;

    protected ScoreCounter scoreCounter;

	// Use this for initialization
	void Start () {
        maxHits = hits;

        scoreCounter = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreCounter>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnPlayerBounceOn(GameObject player)
    {
        this.hits--;
        if (this.hits <= 0)
        {
            if (scoreCounter) scoreCounter.AddScore(this.killScore);
            this.Die();
        }
        else
        {
            if (scoreCounter) scoreCounter.AddScore(this.hitScore);
        }

        GameObject shockWave = Instantiate(particleEffect);
        shockWave.transform.position = this.transform.position;
        
        if (debugScaleOnHit)
        {
            Vector3 scale = this.transform.localScale;
            scale.y = (float)hits / (float)maxHits;
            this.transform.localScale = scale;
        }
    }
}
