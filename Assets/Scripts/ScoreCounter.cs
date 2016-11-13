using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public Text ScoreText1;
    public Text ScoreText2;

    [SerializeField]
    private GameObject comboText;

    private int actualScore = 0;
    private int displayScore = 0;

    public int Score { get { return actualScore; } }

    private float timer = 0;
    private int combo = 0;

	// Update is called once per frame
	void Update ()
    {
        float scoreDifference = actualScore - displayScore;

        if (scoreDifference > 0)
        {
            displayScore += Mathf.CeilToInt(scoreDifference / 20);
        }

        ScoreText1.text = ""+displayScore;
        ScoreText2.text = ""+displayScore;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
        }

        ScoreText1.fontSize = 60 + (int)(timer * 30);
        ScoreText2.fontSize = 60 + (int)(timer * 30);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(1000);
        }
	}

    public void AddScore(int amount)
    {
        actualScore += amount * Mathf.Max(1, combo);
        timer = .3f;
    }

    // Called by enemy when destroyed
    public void NotifyLastHit(Vector3 position)
    {
        combo++;
        if (combo > 1)
        {
            GameObject inst = (GameObject)Instantiate(comboText);
            inst.SendMessage("SetText", "x" + combo);
            inst.transform.position = position;
        }
    }

    // Called by enemy when hit but not destroyed
    public void NotifyDamage(Vector3 position)
    {
        combo = 0;
    }
}
