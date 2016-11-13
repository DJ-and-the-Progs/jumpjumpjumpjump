using UnityEngine;
using System.Collections;

public class ComboText : MonoBehaviour {


    [SerializeField]
    private Vector2 randomAngleLimits = new Vector2(30, 150);
    private float randomAngle;

    [SerializeField]
    private Vector2 randomSpeedLimits = new Vector2(0.4f, 1);
    private float randomSpeed;

    private Vector3 dirVec;

    [SerializeField]
    private AnimationCurve falloffCurve;

    [SerializeField]
    private float lifetime = 2;

    private float startTime;

	// Use this for initialization
	void Start () {
        randomAngle = Random.Range(randomAngleLimits.x, randomAngleLimits.y) * Mathf.PI / 180;
        randomSpeed = Random.Range(randomSpeedLimits.x, randomSpeedLimits.y);

        dirVec = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        startTime = Time.time;

        Vector3 rotation = this.transform.eulerAngles;
        rotation.y = randomAngle;
        this.transform.eulerAngles = rotation;
	}
	
	// Update is called once per frame
	void Update () {
        float time = (Time.time - startTime) / lifetime;

        Vector3 target = this.transform.position;
        target += dirVec * falloffCurve.Evaluate(time) * randomSpeed;
        this.transform.position = target;

        if (time >= 1) Destroy(this.gameObject); 
	}
}
