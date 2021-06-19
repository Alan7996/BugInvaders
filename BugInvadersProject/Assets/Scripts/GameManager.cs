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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.paused;
            UIManager.instance.OnPauseUI();
        }
    }

    public void IncScore(int inc)
    {
        score += inc;
        UIManager.instance.IncScoreUI(inc);
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

        UIManager.instance.OnGameOverUI(isHighScore);
    }

    public void ToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
