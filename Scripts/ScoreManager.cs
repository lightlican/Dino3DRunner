using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int GetCurrentScore()
    {
        return score;
    }
    public static ScoreManager Instance;
    public Text scoreText;
    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        Debug.Log("Очки обновлены: " + score);
    }
}