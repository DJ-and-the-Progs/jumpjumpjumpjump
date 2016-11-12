using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private AnimationCurve bounceCurve;

    [SerializeField]
    private float bounceHeight;

	[SerializeField]
	private float movementVelocity;

	[SerializeField]
	private float movementVelocityThreshhold;

    private float lastJumpFrom; // Last Y height where we jumped off from
    private float jumpTime;

    [SerializeField]
    private float floorLookDistance = 0.01f;
    [SerializeField]
    private float spherecastRadius = 0.4f;
    [SerializeField]
    private float maxCurveTime = 0.5f;

	// Use this for initialization
	void Start () {
        lastJumpFrom = this.transform.position.y;
        jumpTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.time - jumpTime;
        float currentJumpHeight = bounceCurve.Evaluate(currentTime) * bounceHeight;

        Vector3 target = this.transform.position;
        target.y = lastJumpFrom + currentJumpHeight;

		float horizontalInput = Input.GetAxis("Horizontal");

		float changeInPosition = currentTime <= movementVelocityThreshhold ? 0: movementVelocity * horizontalInput ;
        target.x += changeInPosition;
        this.GetComponent<Rigidbody>().MovePosition(target);

        // Look down for collisions with floor
        RaycastHit hit;
        Color debugHitColor = Color.green;
        if (currentTime > maxCurveTime && Physics.SphereCast(this.transform.position, spherecastRadius, Vector3.down, out hit, floorLookDistance))
        {
            debugHitColor = Color.red;
            lastJumpFrom = hit.point.y + 1;
            jumpTime = Time.time;
        }

        if (currentTime < maxCurveTime && Physics.SphereCast(this.transform.position, spherecastRadius, Vector3.up, out hit, floorLookDistance))
        {
            jumpTime = Time.time - 0.5f;
            lastJumpFrom = hit.point.y - 1 - bounceCurve.Evaluate(0.5f) * bounceHeight;
        }
        Debug.DrawRay(this.transform.position, Vector3.down * floorLookDistance, debugHitColor, 0.5f);

        Debug.DrawRay(this.transform.position, Vector3.down * (this.transform.position.y - lastJumpFrom), Color.blue);
	}

    void OnCollisionEnter(Collision col)
    {
        //lastJumpFrom = this.transform.position.y;
        //jumpTime = Time.time;
    }
}
