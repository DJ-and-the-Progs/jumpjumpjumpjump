using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	[SerializeField]
    private Transform target;
    private PlayerMovement player;


	[SerializeField]
	private float cameraHeightThresholdPercent;

    private float playerLowerBound = .0f;
    private float playerUpperBound = .5f;
    private float playerLeftBound = -.2f;
    private float playerRightBound = .2f;

    [SerializeField]
    [Tooltip("Left-most camera position")]
    private float minX;
    [SerializeField]
    [Tooltip("Right-most camera position")]
    private float maxX;

    [SerializeField]
    private Vector2 maxRotationOffset;
    [SerializeField]
    [Tooltip("How much translational difference is required to reach max rotational offset on x and y")]
    private Vector2 rotationTranslationalDifferenceTarget;

	// Use this for initialization
	void Start () {
        Debug.Assert(target != null);
        player = target.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.FirstEntering || player.Dead) return;

        Vector3 position = this.transform.position;
        Vector2 screenCenter = new Vector2(Screen.width, Screen.height) / 2;
        Vector2 targetPosition = Camera.main.WorldToScreenPoint(target.position);
        Vector2 characterOffsetFromCenter = (targetPosition - screenCenter);



        // Get difference between object and bounds if it is outside those bounds.
        if(characterOffsetFromCenter.x > screenCenter.x * playerRightBound) {
            characterOffsetFromCenter.x -= screenCenter.x * playerRightBound;
        }
        else if (characterOffsetFromCenter.x < screenCenter.x * playerLeftBound) {
            characterOffsetFromCenter.x -= screenCenter.x * playerLeftBound;
        }
        else {
            characterOffsetFromCenter.x = 0;
        }

        if (characterOffsetFromCenter.y > screenCenter.y * playerUpperBound) {
            characterOffsetFromCenter.y -= screenCenter.y * playerUpperBound;
        }
        else if (characterOffsetFromCenter.y < screenCenter.y * playerLowerBound) {
            characterOffsetFromCenter.y -= screenCenter.y * playerLowerBound;
        }
        else {
            characterOffsetFromCenter.y = 0;
        }

        if (characterOffsetFromCenter.sqrMagnitude > 0) {
            position += new Vector3(characterOffsetFromCenter.x, characterOffsetFromCenter.y, 0) * Time.deltaTime;
            position.z = transform.position.z;
            position.x = Mathf.Clamp(position.x, minX, maxX);
        }

        this.transform.position = position;


        Vector3 rotation = this.transform.eulerAngles;
        Vector3 diff = target.position - this.transform.position;

        rotation.y = Mathf.Clamp(diff.x / this.rotationTranslationalDifferenceTarget.x, -1, 1) * maxRotationOffset.x;
        rotation.x = -Mathf.Clamp(diff.y / this.rotationTranslationalDifferenceTarget.y, -1, 1) * maxRotationOffset.y;
        this.transform.eulerAngles = rotation;
	}
}
