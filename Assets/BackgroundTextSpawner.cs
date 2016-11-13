using UnityEngine;
using System.Collections;

public class BackgroundTextSpawner : MonoBehaviour {

    public GameObject textPrefab;

    private GameObject[] backgroundTextObjects = new GameObject[256];
    private Banner[] banners = new Banner[256];

	// Use this for initialization
	void Start () {

        for(int i = 0; i < 256; i++)
        {
            // spawn background text:
            backgroundTextObjects[i] = (GameObject)Instantiate(textPrefab, transform);
            banners[i] = backgroundTextObjects[i].GetComponent<Banner>();

            // pick a random starting position/direction
            int startX = (int)(Random.value * 200 - 100);
            int startY = (int)(Random.value * 50 - 25) * 3;

            backgroundTextObjects[i].transform.position = new Vector3(startX, startY, 20);
            banners[i].velocity = new Vector3((int)(Random.value*2) % 2 * 10 - 5, 0, 0);
        }
	}
}
