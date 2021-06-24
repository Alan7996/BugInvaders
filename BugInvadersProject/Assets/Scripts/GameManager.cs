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


    public int score = 0;

    private void Start()
    {
        gameState = GameState.playing;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
        }
        else
        {
            score = PlayerPrefs.GetInt("CurrentScore");
        }

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerController.instance.ChangePos(new Vector3(0,-10,0));

            UIManager.instance.IncScoreUI(score);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState != GameState.paused)
            {
                gameState = GameState.paused;
                UIManager.instance.OnPauseUI();
            } else
            {
                UIManager.instance.OnContinueClickUI();
            }
        }
    }

    public void IncScore(int inc)
    {
        score += inc;
        UIManager.instance.IncScoreUI(score);
    }

    public void OnGameOver()
    {
        gameState = GameState.gameOver;
        EnemySpawner.instance.StopCoroutine();

        int highscore = PlayerPrefs.GetInt("Highscore");
        bool isHighScore = score > highscore;
        if (isHighScore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }

        PlayerPrefs.SetInt("CurrentScore", 0);

        UIManager.instance.OnGameOverUI(isHighScore);
    }

    public void OnStageClear()
    {
        gameState = GameState.gameOver;
        PlayerPrefs.SetInt("CurrentScore", score);
        UIManager.instance.OnStageClearUI();
        PlayerController.instance.OnPausePlayer();
    }

    public void ToStartMenu()
    {
        Destroy(PlayerController.instance.gameObject);
        Destroy(UIManager.instance.gameObject);
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
