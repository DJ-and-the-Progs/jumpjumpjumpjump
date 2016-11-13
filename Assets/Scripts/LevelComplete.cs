using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour {

    [SerializeField]
    private GameObject scoreKeeper;

    private Animator anim;
    private bool revealed = false;
    private int score;
    private int displayScore;

    [SerializeField]
    private Text scoreBox;
    [SerializeField]
    private float timeToCount = 3.5f;
    private float startCountingTime;

    private LevelInfo level;

    // Use this for initialization
    void Start() {
        this.anim = GetComponent<Animator>();
        level = LevelInfo.GetLevelInfo();
    }

    // Update is called once per frame
    void Update() {
        if (revealed)
        {
            int lastDisplay = displayScore;
            if (displayScore < score)
            {
                displayScore = (int)Mathf.Lerp(0, score, (Time.time - startCountingTime) / timeToCount);
            }

            for (int i = 0; i < level.StarScores.Length; i++)
            {
                int s = level.StarScores[i];
                if (displayScore >= s && lastDisplay < s)
                {
                    anim.SetInteger("stars", i + 1);
                }
            }

            scoreBox.text = "Score: " + ("" + displayScore).PadLeft(5, '0');

        }
    }

    public void Reveal()
    {
        anim.SetTrigger("start");
        score = scoreKeeper.GetComponent<ScoreCounter>().Score;
        scoreKeeper.SetActive(false);
    }

    // Called by animation, starts the counter thing
    public void StartCounting()
    {
        revealed = true;
        startCountingTime = Time.time;
    }


    public void OnNextLevelClicked()
    {
        Application.LoadLevel(level.NextLevel);
    }
    public void OnMenuClicked()
    {
        Application.LoadLevel("main menu");
    }
}
