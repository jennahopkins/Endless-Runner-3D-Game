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
    [SerializeField] Button pauseButton;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;
    public bool GameOver => gameOver;

    void Start()
    {
        // initialize and pause for the start panel
        timeLeft = startTime;
        PauseGame("Start");
        pauseButton.gameObject.SetActive(false);
        pauseButton.onClick.AddListener(() => PauseGame("Resume"));
    }

    void Update()
    {
        DecreaseTime();
    }

    public void PauseGame(string reason)
    {
        // show start/pause panel and pause game
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0f;
        startPanel.SetActive(true);
        startButton.GetComponentInChildren<TextMeshProUGUI>().text = reason;
        startButton.onClick.AddListener(ResumeGame);
        music.Pause();
    }
    
    public void ResumeGame()
    {
        // hide start/pause panel and resume game
        pauseButton.gameObject.SetActive(true);
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
        // decrease time and check for game over
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
        // when game over, show UI and stop player movement
        gameOver = true;
        playerController.enabled = false;
        gameOverUI.SetActive(true);
        restartButton.onClick.AddListener(RestartGame);
        Time.timeScale = .1f;
        int score = FindAnyObjectByType<ScoreManager>().GetScore();
        HighScoreManager highScoreManager = FindAnyObjectByType<HighScoreManager>();
        highScoreManager.TrySetNewHighScore(score);
    }

    void RestartGame()
    {
        // when restart button is clicked, reload the scene and reset game state
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameOver = false;
        playerController.enabled = true;
        gameOverUI.SetActive(false);
        timeLeft = startTime;
    }
}
