using UnityEngine;
using System.Collections;

public class MoveToPortal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = this.transform.position;

		Vector3 portalPosition = GameObject.FindGameObjectWithTag("Portal").transform.position;

		position = Vector3.MoveTowards(position, portalPosition, 0.5f);

		this.transform.position = position;
	}

	public void DestroyPlayer()
	{
		Destroy(this.gameObject);
		Destroy(this);
		Destroy(GameObject.FindObjectOfType<CameraFollow>());
	}
}
