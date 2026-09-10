using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Lives")]
    public int lives = 3;
    public Image[] hearts;

    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject winPanel;

    [Header("Score")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("Timer")]
    public TextMeshProUGUI timeText;

    [Header("Levels")]
    public int currentLevel = 1;
    public GridManager gridManager;

    [Header("Result Text")]
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverTimeText;
    public TextMeshProUGUI gameOverLevelText;

    public TextMeshProUGUI winScoreText;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI winLevelText;

    private int currentNumber = 1;
    private int numbersToComplete;

    private float timer;
    private bool timerRunning = true;
    private bool gameOver = false;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        // Hide panels at the beginning
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (BGMManager.instance != null)
            BGMManager.instance.PlayGameMusic();

        // Setup first level
        SetupLevel();

        UpdateLives();
        UpdateScore();

        timer = 0f;
        UpdateTime();
    }


    void Update()
    {
        if (timerRunning)
        {
            timer += Time.deltaTime;
            UpdateTime();
        }
    }


    // =========================================
    // LEVEL SETUP
    // =========================================

    void SetupLevel()
    {
        currentNumber = 1;

        if (currentLevel == 1)
        {
            numbersToComplete = 7;
        }
        else if (currentLevel == 2)
        {
            numbersToComplete = 8;
        }
        else if (currentLevel == 3)
        {
            numbersToComplete = 10;
        }
    }


    // =========================================
    // TIMER
    // =========================================

    void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        if (timeText != null)
        {
            timeText.text =
                minutes.ToString("00") + ":" +
                seconds.ToString("00");
        }
    }


    // =========================================
    // SCORE
    // =========================================

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }
    }


    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }


    // =========================================
    // TILE CHECKING
    // =========================================

    public void CheckTile(Tile tile)
    {
        if (gameOver)
            return;


        // =====================================
        // X TILE = IMMEDIATE GAME OVER
        // =====================================

        if (tile.isX)
        {
            tile.WrongShake();

            ShowGameOver();

            return;
        }


        // =====================================
        // CORRECT NUMBER
        // =====================================

        if (tile.number == currentNumber)
        {
            score += 10;
            UpdateScore();

            if (SoundManager.instance != null)
                SoundManager.instance.PlayCorrect();

            tile.CorrectEffect();

            currentNumber++;


            // Check if current level is complete
            if (currentNumber > numbersToComplete)
            {
                CompleteLevel();
            }
        }


        // =====================================
        // WRONG NUMBER
        // =====================================

        else
        {
            tile.WrongEffect();

            if (SoundManager.instance != null)
                SoundManager.instance.PlayWrong();

            // This tile gets its own timer
            HideWrongTileAfterDelay(tile);

            LoseLife();
        }
    }


    // =========================================
    // COMPLETE LEVEL
    // =========================================

    void CompleteLevel()
    {

        // =====================================
        // LEVEL 3 COMPLETE = VICTORY
        // =====================================

        if (currentLevel >= 3)
        {
            ShowWin();
            return;
        }


        // =====================================
        // MOVE TO NEXT LEVEL
        // =====================================

        currentLevel++;

        SetupLevel();

        // Tell GridManager to create
        // the new level's tiles
        if (gridManager != null)
        {
            gridManager.LoadNextLevel();
        }
    }


    // =========================================
    // LIVES
    // =========================================

    void LoseLife()
    {
        lives--;

        UpdateLives();

        if (lives <= 0)
        {
            ShowGameOver();
        }
    }


    void UpdateLives()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }


    // =========================================
    // WRONG TILE
    // =========================================

    void HideWrongTileAfterDelay(Tile tile)
    {
        StartCoroutine(HideWrongTileRoutine(tile));
    }


    IEnumerator HideWrongTileRoutine(Tile tile)
    {
        yield return new WaitForSeconds(1.2f);

        if (tile != null)
        {
            tile.HideAgain();
        }
    }


    // =========================================
    // GAME OVER
    // =========================================

    void ShowGameOver()
    {
        gameOver = true;
        timerRunning = false;

        // Update Game Over result information
        if (gameOverScoreText != null)
            gameOverScoreText.text = "Score : " + score;

        if (gameOverTimeText != null)
            gameOverTimeText.text = "Time : " + FormatTime();

        if (gameOverLevelText != null)
            gameOverLevelText.text = "Level : " + currentLevel;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }


    // =========================================
    // WIN
    // =========================================

    void ShowWin()
    {
        gameOver = true;
        timerRunning = false;

        // Update Victory result information
        if (winScoreText != null)
            winScoreText.text = "Score : " + score;

        if (winTimeText != null)
            winTimeText.text = "Time : " + FormatTime();

        if (winLevelText != null)
            winLevelText.text = "Level : " + currentLevel;

        if (winPanel != null)
            winPanel.SetActive(true);
    }

    // =========================================
    // TIME FORMATTING
    // =========================================

    string FormatTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        return minutes.ToString("00") + ":" +
               seconds.ToString("00");
    }


    // =========================================
    // RESTART
    // =========================================

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // =========================================
    // MAIN MENU
    // =========================================

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}