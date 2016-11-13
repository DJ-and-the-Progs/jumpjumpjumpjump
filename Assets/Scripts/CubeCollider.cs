using UnityEngine;
using System.Collections;

public class CubeCollider : MonoBehaviour {

	[SerializeField]
	private GameObject particleEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col)
	{
		Instantiate(particleEffect).transform.position = this.transform.position;
	}
}
