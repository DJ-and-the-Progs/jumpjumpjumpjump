using UnityEngine;
using System.Collections;

public class PortalCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		collider.gameObject.SendMessage("OnPortalCollide");
	}
}
