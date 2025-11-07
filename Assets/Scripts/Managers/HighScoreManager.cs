using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    private const string HighScoreKey = "HighScore";

    void Awake()
    {
        // load and display high score on awake
        float highScore = GetHighScore();
        SetText((int)highScore);
    }

    public void SetText(int score)
    {
        highScoreText.text = "High Score: " + score.ToString();
    }

    public float GetHighScore()
    {
        return PlayerPrefs.GetFloat(HighScoreKey, 0f);
    }

    public void TrySetNewHighScore(float newScore)
    {
        // check and set new high score if applicable
        float currentHigh = GetHighScore();

        if (newScore > currentHigh)
        {
            PlayerPrefs.SetFloat(HighScoreKey, newScore);
            PlayerPrefs.Save();
        }
    }
}