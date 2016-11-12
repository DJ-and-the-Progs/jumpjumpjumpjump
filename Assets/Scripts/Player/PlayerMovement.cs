using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private AnimationCurve bounceCurve;

    [SerializeField]
    private float bounceHeight;

    private float lastJumpFrom; // Last Y height where we jumped off from
    private float jumpTime;

	// Use this for initialization
	void Start () {
        lastJumpFrom = this.transform.position.y;
        jumpTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.time - jumpTime;
        float currentJumpHeight = bounceCurve.Evaluate(currentTime) * bounceHeight;

        Vector3 position = this.transform.position;
        position.y = lastJumpFrom + currentJumpHeight;
        this.transform.position = position;
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("COLLISION");
        lastJumpFrom = this.transform.position.y;
        jumpTime = Time.time;
    }
}
