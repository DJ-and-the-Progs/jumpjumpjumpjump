using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int hits = 1;

    private int maxHits;

	// Use this for initialization
	void Start () {
        maxHits = hits;
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
            this.Die();

        Vector3 scale = this.transform.localScale;
        scale.y = (float)hits / (float)maxHits;
        this.transform.localScale = scale;
    }
}
