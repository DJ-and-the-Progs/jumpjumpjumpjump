using UnityEngine;
using System.Collections;

public class SpinLyric : MonoBehaviour {

    [SerializeField]
    private float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rot = this.transform.eulerAngles;
        rot.y += speed;
        this.transform.eulerAngles = rot;
	}
}
