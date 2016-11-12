using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private AnimationCurve fallCurve;

	[SerializeField]
    private Transform target;

	[SerializeField]
	private float cameraHorizontalFollowSpeed;

	[SerializeField]
	private float cameraHeightThresholdPercent;

	[SerializeField]
	private float cameraRiseSpeed;

	private float cameraAcceleration;

	private float fallingTime;

	// Use this for initialization
	void Start () {
        Debug.Assert(target != null);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = this.transform.position;

		//position.x = Mathf.Lerp(position.x, target.position.x, 0.0005f);
		// switched to moveTorwards because has a cool mvoing affect
		position.x = Mathf.MoveTowards(position.x, target.position.x, cameraHorizontalFollowSpeed);

		float targetYPosition = Camera.main.WorldToScreenPoint(target.position).y;

		// calculates threshold depth for character position to not exceed from center
		float screenCenter = Screen.height / 2;
		float thresholdDepth = Screen.height - Screen.height * cameraHeightThresholdPercent;

		//needs to calculate new camera acceleration if falling
		fallingTime = (targetYPosition < screenCenter - thresholdDepth) ? fallingTime + Time.deltaTime : 0;
		cameraAcceleration = (targetYPosition < screenCenter - thresholdDepth) ? cameraAcceleration+fallCurve.Evaluate(fallingTime) : 0;

		if (targetYPosition > screenCenter + thresholdDepth)
		{
			position.y = Mathf.MoveTowards(position.y, target.position.y, cameraRiseSpeed);
		}
		else if (targetYPosition < screenCenter - thresholdDepth)
		{
			position.y = Mathf.MoveTowards(position.y, target.position.y, cameraAcceleration);
		}

			this.transform.position = position;
	}
}
