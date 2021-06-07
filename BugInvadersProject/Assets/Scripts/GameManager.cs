using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    public TMP_Text scoreText;
    // temporary usage of highscore text to check funtionality
    public TMP_Text highscoreText;

    private int score = 0;

    private void Start()
    {
        scoreText.text = "SCORE : " + score;
        highscoreText.enabled = false;
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
        int highscore = PlayerPrefs.GetInt("Highscore");
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }

        highscoreText.text = highscore.ToString();
        highscoreText.enabled = true;
    }
}
