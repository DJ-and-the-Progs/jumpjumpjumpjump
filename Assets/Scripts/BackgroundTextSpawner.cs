using UnityEngine;
using System.Collections;

public class BackgroundTextSpawner : MonoBehaviour {

    public GameObject textPrefab;

    private GameObject[] backgroundTextObjects = new GameObject[256];
    private Vector3[] velocities = new Vector3[256];
	// Use this for initialization
	void Start () {

        for(int i = 0; i < 256; i++)
        {
            // spawn background text:
           backgroundTextObjects[i] = (GameObject)Instantiate(textPrefab, transform);
            //banners[i] = backgroundTextObjects[i].GetComponent<Banner>();

            // pick a random starting position/direction
            int startX = (int)(Random.value * 200 - 100);
            int startY = (int)(Random.value * 50 - 25) * 3;

            backgroundTextObjects[i].transform.position = new Vector3(startX, startY, 20);
            velocities[i] = new Vector3((int)(Random.value*2) % 2 * 10 - 5, 0, 0);
        }
	}

    void Update()
    {
        

        for(int i = 0; i < 256; i++)
        {
            // Update the position of the object:
            GameObject go = backgroundTextObjects[i];
            go.transform.Translate(velocities[i] * Time.deltaTime);
        
            Vector3 position = go.transform.position;
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);


            if (screenPosition.x > Screen.width * 1.5f) {
                position.x -= 200;
                go.transform.position = position;
            }
            if (screenPosition.x < -Screen.width * .5f) {
                position.x += 200;
                go.transform.position = position;
            }
            if (screenPosition.y > Screen.height * 2.0f) {
                position.y -= 150;
                go.transform.position = position;
            }
            if (screenPosition.y < -Screen.height * 1.0f) {
                position.y += 150;
                go.transform.position = position;
            }
        }
    }
}
