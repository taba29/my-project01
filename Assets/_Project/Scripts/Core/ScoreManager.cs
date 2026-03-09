using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager I;

    private int score;

    void Awake()
    {
        I = this;
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
}