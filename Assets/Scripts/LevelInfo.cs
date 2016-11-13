using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

    [SerializeField]
    private int maxScore;
    public int MaxScore { get { return maxScore; } }

    [SerializeField]
    [Tooltip("Scores to trigger stars (expects 3 values)")]
    private int[] starScores;
    public int[] StarScores { get { return starScores; } }

    [SerializeField]
    private string nextLevel;
    public string NextLevel { get { return nextLevel; } }

    public static LevelInfo GetLevelInfo()
    {
        return  GameObject.FindGameObjectWithTag("World").GetComponent<LevelInfo>();
    }
}
