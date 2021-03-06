﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private AnimationCurve bounceCurve;

    [SerializeField]
    private float bounceHeight;

    [SerializeField]
    private float movementAccel;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    [Tooltip("Minimum velocity before just set to zero")]
    private float minVelocityCuttoff;
    [SerializeField]
    private float movementDampening = 0.95f;
    private float horizVelocity;

	[SerializeField]
	private float movementVelocityThreshhold;

	private float rotateTorwards = 0;
	private float lastJumpFrom; // Last Y height where we jumped off from
    private float jumpTime;

    [SerializeField]
    private float floorLookDistance = 0.01f;
    [SerializeField]
    private float spherecastRadius = 0.4f;
    [SerializeField]
    private float maxCurveTime = 0.5f;

	[SerializeField]
	private float rotationSpeed = 0.5f;

	[SerializeField]
    [Tooltip("Used for first jumping into the scene")]
    private float staringVelocity = 0.1f;
    private bool firstEntering = true;
    public bool FirstEntering { get { return firstEntering; } }
    [SerializeField]
    private float maxStartingX;

	[SerializeField]
    private Animator lyricAnimator;

    private bool dead = false;
    public bool Dead { get { return dead; } set { dead = value; } }

    [SerializeField]
    private float adjustmentDistance = 3f;
    [SerializeField]
    private float adjustmentLookRadius = 2.12f;
    [SerializeField]
    private float adjustmentRate = 5f;

	// Use this for initialization
	void Start () {
        jumpTime = Time.time - maxCurveTime;
        lastJumpFrom = this.transform.position.y - bounceCurve.Evaluate(maxCurveTime) * bounceHeight;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 target = this.transform.position;

        // Find where we are in jump
        float currentTime = Time.time - jumpTime;
        float currentJumpHeight = bounceCurve.Evaluate(currentTime) * bounceHeight;
        target.y = lastJumpFrom + currentJumpHeight;

		// Player movement control
		float inputAxis = Input.GetAxisRaw("Horizontal");
		float horizontalInput = inputAxis * (firstEntering ? 0:1);
        // Only damp if not trying to move (With tolerance)
        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            this.horizVelocity *= movementDampening;
            if (Mathf.Abs(this.horizVelocity) < this.minVelocityCuttoff)
                this.horizVelocity = 0;
        }
        this.horizVelocity += horizontalInput * movementAccel * Time.deltaTime;
        this.horizVelocity = Mathf.Clamp(this.horizVelocity, -maxVelocity, maxVelocity);
        if (firstEntering) this.horizVelocity = staringVelocity;

		// Player movement angle
		Quaternion currentRotation = this.transform.rotation;
		Vector3 currentRotationEulerAngles = currentRotation.eulerAngles;
		float currentAngle = currentRotation.eulerAngles.y;

		if (Mathf.Abs(horizVelocity) >= Mathf.Abs(maxVelocity/2))
		{
			if (horizontalInput < 0)
			{
				rotateTorwards = 180;
			}
			if (horizontalInput > 0)
			{
				rotateTorwards = 0;
			}
		}

		currentAngle = Mathf.MoveTowards(currentAngle, rotateTorwards, rotationSpeed);

		currentRotationEulerAngles.y = currentAngle;
		currentRotation.eulerAngles = currentRotationEulerAngles;
		this.transform.rotation = currentRotation;





		float horizontalTarget = this.CheckHorizontalMovement(horizVelocity * Time.deltaTime);
        if (horizontalTarget == 0) this.horizVelocity = 0; // Reset vel, hitting wall

		float changeInPosition = currentTime <= movementVelocityThreshhold ? 0: horizontalTarget ;
        target.x += changeInPosition;
        if (firstEntering) target.x = Mathf.Min(maxStartingX, target.x);

        lyricAnimator.SetBool("falling", this.IsGoingDown());
        

        // Look down for collisions with floor
        RaycastHit hit;
        Color debugHitColor = Color.green;
        float lookAheadDistance = floorLookDistance + Mathf.Abs(bounceCurve.Evaluate(currentTime + Time.deltaTime) * bounceHeight - currentJumpHeight);
        int layerMask = LayerMask.GetMask("Default"); // What layers to look for detection
        if (this.IsGoingDown() && Physics.SphereCast(this.transform.position, spherecastRadius, Vector3.down, out hit, lookAheadDistance, layerMask))
        {
            debugHitColor = Color.red;
            // Reset velocity from scripted velocity based on current input
            if (firstEntering) this.horizVelocity = Input.GetAxisRaw("Horizontal") * maxVelocity;
            firstEntering = false;

            lastJumpFrom = hit.point.y + 1;
            jumpTime = Time.time;

            // Tell the thing we bounced on that we bounced on it
            object[] bounceData = new object[] { this.gameObject, hit.point };
            hit.collider.gameObject.SendMessage("OnPlayerBounceOn", bounceData, SendMessageOptions.DontRequireReceiver);
        }

        if (!this.IsGoingDown() && Physics.SphereCast(this.transform.position, spherecastRadius, Vector3.up, out hit, lookAheadDistance, layerMask))
        {
            jumpTime = Time.time - 0.5f;
            lastJumpFrom = hit.point.y - 1 - bounceCurve.Evaluate(0.5f) * bounceHeight;
        }
        // Ray to show whether we detected floor hit
        Debug.DrawRay(this.transform.position, Vector3.down * floorLookDistance, debugHitColor, 0.5f);
        // Line to show where we bounced from (last floor)
        Debug.DrawRay(this.transform.position, Vector3.down * (this.transform.position.y - lastJumpFrom), Color.blue);

        if (this.IsGoingDown() && Physics.SphereCast(this.transform.position, adjustmentLookRadius, Vector3.down, out hit, adjustmentDistance, layerMask))
        {
            float diff = (hit.collider.transform.position.x-this.transform.position.x);
            target.x += Mathf.Min(Mathf.Abs(diff), adjustmentRate * Time.deltaTime) * Mathf.Sign(diff);
        }
        Debug.DrawRay(this.transform.position, Vector3.down * adjustmentDistance, Color.cyan);

        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        //rb.MovePosition(target);
        this.transform.position = target;
	}

    /// <summary>
    /// Return whether player is currently falling downward
    /// </summary>
    /// <returns></returns>
    public bool IsGoingDown()
    {
        return Time.time - jumpTime > maxCurveTime;
    }


    public float CheckHorizontalMovement(float horizontalTarget)
    {
        int layerMask = LayerMask.GetMask("Default");
        float distance = Mathf.Abs(horizontalTarget);

        // Check head
        RaycastHit hit;
        Vector3 headStart = this.transform.position + Vector3.up * 0.5f;
        Vector3 feetStart = this.transform.position + Vector3.down * 0.5f;
        if (Physics.SphereCast(headStart, 0.5f, Vector3.right * Mathf.Sign(horizontalTarget), out hit, distance, layerMask))
        {
            horizontalTarget = 0; // Mathf.Min(Mathf.Abs(horizontalTarget), Mathf.Abs(this.transform.position.x - hit.point.x)) * Mathf.Sign(horizontalTarget);
        }

        // Check feet
        else if (Physics.SphereCast(feetStart, 0.5f, Vector3.right * Mathf.Sign(horizontalTarget), out hit, distance, layerMask))
        {
            horizontalTarget = 0;
        }
        return horizontalTarget;
    }

	public void OnPortalCollide()
	{
		Destroy(this);
		this.GetComponent<MoveToPortal>().enabled = true;
		lyricAnimator.SetTrigger("OnPortalCollide");

        GameObject.FindGameObjectWithTag("LevelComplete").GetComponent<LevelComplete>().Reveal();

	}
}
