using UnityEngine;
using System.Collections;

public class Banner : MonoBehaviour {

    public Vector3 velocity;

	// Update is called once per frame
    void Update() {
        transform.Translate(velocity * Time.deltaTime);
    }
}
