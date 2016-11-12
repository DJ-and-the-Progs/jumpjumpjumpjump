using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;

	// Use this for initialization
	void Start () {
        Debug.Assert(target != null);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(position.x, target.position.x, 0.25f);

        this.transform.position = position;
	}
}
