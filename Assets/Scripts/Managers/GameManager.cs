using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] GameObject startPanel;
    [SerializeField] Button startButton;
    [SerializeField] AudioSource music;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;
    bool gamePaused = false;

    public bool GameOver => gameOver;

    void Start() 
    {
        timeLeft = startTime;
        PauseGame("Start");
    }

    void Update()
    {
        DecreaseTime();
    }

    public void PauseGame(string reason)
    {
        gamePaused = true;
        Time.timeScale = 0f;
        startPanel.SetActive(true);
        startButton.GetComponentInChildren<TextMeshProUGUI>().text = reason;
        startButton.onClick.AddListener(ResumeGame);
        music.Pause();
    }
    
    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        startPanel.SetActive(false);
        startButton.onClick.RemoveListener(ResumeGame);
        music.UnPause();
    }

    public void IncreaseTime(float amount) 
    {
        timeLeft += amount;
    }

    void DecreaseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver() 
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverUI.SetActive(true);
        restartButton.onClick.AddListener(RestartGame);
        Time.timeScale = .1f;
        int score = FindObjectOfType<ScoreManager>().GetScore();
        HighScoreManager highScoreManager = FindObjectOfType<HighScoreManager>();
        highScoreManager.TrySetNewHighScore(score);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameOver = false;
        playerController.enabled = true;
        gameOverUI.SetActive(false);
        timeLeft = startTime;
    }
}
