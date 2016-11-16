using UnityEngine;
using System.Collections;

public class SeeSawBlock : MonoBehaviour {

	[SerializeField]
	private AudioClip audioClip;

	[SerializeField]
    private GameObject particleEffect;


    protected ScoreCounter scoreCounter;

    [SerializeField]
    private SeeSawBlock other;

    [SerializeField]
    private Color[] colors;

    private int offset = 0;
    [SerializeField]
    private int maxOffset = 2;

    [SerializeField]
    private Renderer myRenderer;

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

    protected virtual void OnPlayerBounceOn(object[] data)
    {
        // GameObject player = (GameObject)data[0];
        // Vector3 hitPoint = (Vector3)data[1];

        if (scoreCounter)
        {
            scoreCounter.NotifyDamage(new Vector3());
        }

        GameObject shockWave = Instantiate(particleEffect);
        shockWave.transform.position = this.transform.position;
        
        if (other && offset > -maxOffset) {
            offset--;
            transform.position -= new Vector3(0, 1.25f, 0);
            myRenderer.material.color = colors[Mathf.Abs(offset)];
            
            other.offset++;
            other.transform.position += new Vector3(0, 1.25f, 0);
            other.myRenderer.material.color = colors[Mathf.Abs(offset)];
        }


		AudioSource.PlayClipAtPoint(this.audioClip, this.transform.position);
	}
}
