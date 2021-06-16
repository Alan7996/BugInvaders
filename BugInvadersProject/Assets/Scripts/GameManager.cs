using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    playing, paused, gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;

    public GameState gameState;

    public TMP_Text scoreText;
    public TMP_Text resultScoreText;
    public TMP_Text highscoreText;
    public GameObject gameOverImage;
    public TMP_Text newHighScoreText;
    public Button startMenuBtn;

    private int score = 0;

    private void Start()
    {
        gameState = GameState.playing;
        newHighScoreText.enabled = false;
        gameOverImage.SetActive(false);
        scoreText.text = "SCORE : " + score;
        startMenuBtn.onClick.AddListener(OnStartMenuClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void IncScore(int inc)
    {
        score += inc;
        scoreText.text = "SCORE : " + score;
    }

    public void OnGameOver()
    {
        gameState = GameState.gameOver;
        EnemySpawner.instance.StopCoroutine();

        int highscore = PlayerPrefs.GetInt("Highscore");
        if (score > highscore)
        {
            newHighScoreText.enabled = true;
            highscore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }

        resultScoreText.text = "SCORE : " + score;
        highscoreText.text = "HIGHSCORE : " + highscore.ToString();

        gameOverImage.SetActive(true);
    }

    private void OnStartMenuClick()
    {
        SceneManager.LoadScene(0);
    }
}
