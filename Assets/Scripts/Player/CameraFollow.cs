using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;

	[SerializeField]
	private float cameraHeightThresholdPercent;

	private Camera mainCamera;
	private float targetYPosition;
	private float thresholdDepth;
	private float screenCenter;

	// Use this for initialization
	void Start () {
        Debug.Assert(target != null);
		mainCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(position.x, target.position.x, 0.25f);

		// gets main camera every time incase camera's are changed
		// stored as instance variable so local variable space does not need to be reallocated on every update
		mainCamera = Camera.main;

		targetYPosition = mainCamera.WorldToScreenPoint(target.position).y;

		// calculates threshold depth for character position to not exceed from center
		screenCenter = Screen.height / 2;
		thresholdDepth = Screen.height - Screen.height * cameraHeightThresholdPercent;
		if (targetYPosition > screenCenter + thresholdDepth || targetYPosition < screenCenter - thresholdDepth)
		{
			position.y += target.position.y*0.05f;
		}

		this.transform.position = position;
	}
}
