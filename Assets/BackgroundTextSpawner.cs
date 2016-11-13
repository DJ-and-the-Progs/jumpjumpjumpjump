using UnityEngine;
using System.Collections;

public class BackgroundTextSpawner : MonoBehaviour {

    public GameObject textPrefab;

    float timer = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer > .02) {
            timer = 0;

            // spawn background text:
            GameObject spawned = Instantiate(textPrefab);
            Banner b = spawned.GetComponent<Banner>();
            
            // pick a random starting position/direction
            int startX = (int)(Random.value * 2);
            startX = startX * 100 - 50;
            int startY = (int)(Random.value * 50 - 20);

            spawned.transform.position = new Vector3(startX, startY, 20);
            b.velocity = new Vector3(-startX / 5, 0, 0);
        }

	}
}
