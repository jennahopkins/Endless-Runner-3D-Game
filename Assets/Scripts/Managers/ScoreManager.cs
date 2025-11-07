using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] HighScoreManager highScoreManager;
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    public void IncreaseScore(int amount)
    {
        // increase score if game is not over
        if (gameManager.GameOver) return;

        score += amount;
        scoreText.text = score.ToString();

        if (score > highScoreManager.GetHighScore())
        {
            highScoreManager.SetText(score);
        }
    }
    
    public int GetScore()
    {
        return score;
    }
}
