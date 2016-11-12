using UnityEngine;
using System.Collections;

public class PatrollingMovement : MonoBehaviour {

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float period;

    private Vector3 startPosition;
    private float startTime;

    [SerializeField]
    private AnimationCurve curve;

	// Use this for initialization
	void Start () {
        startPosition = this.transform.position;
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float timeInPeriod = (Time.time - startTime) % period / period;
        this.transform.position = startPosition + direction * curve.Evaluate(timeInPeriod);
	}
}
