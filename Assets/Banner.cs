using UnityEngine;
using System.Collections;

public class Banner : MonoBehaviour {

    public Vector3 velocity;

	// Update is called once per frame
    void Update() {
        transform.Translate(velocity * Time.deltaTime);
        
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 position = transform.position;

        if (screenPosition.x > Screen.width * 1.5f) {
            position.x -= 200;
            transform.position = position;
        }
        if (screenPosition.x < -Screen.width * .5f) {
            position.x += 200;
            transform.position = position;
        }
        if (screenPosition.y > Screen.height * 2.0f) {
            position.y -= 150;
            transform.position = position;
        }
        if (screenPosition.y < -Screen.height * 1.0f) {
            position.y += 150;
            transform.position = position;
        }
    }
}
