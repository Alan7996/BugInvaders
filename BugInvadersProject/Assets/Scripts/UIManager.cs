using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (u_instance == null)
            {
                u_instance = FindObjectOfType<UIManager>();
            }
            return u_instance;
        }
    }

    private static UIManager u_instance;

    public TMP_Text scoreText;

    public GameObject pauseImage;
    public Button continueBtn;
    public Button optionsBtn;
    public Button exitBtn;

    public GameObject volumeImage;
    public Button backBtn;

    public GameObject gameOverImage;
    public TMP_Text resultScoreText;
    public TMP_Text highscoreText;
    public TMP_Text newHighScoreText;
    public Button startMenuBtn;

    public GameObject stageClearImage;
    public TMP_Text currentScoreText;
    public Button nextStageBtn;

    public GameObject machineGunPossible;
    public GameObject missilePossible;
    public GameObject laserPossible;

    // Start is called before the first frame update
    void Start()
    {
        pauseImage.SetActive(false);
        volumeImage.SetActive(false);

        newHighScoreText.enabled = false;
        gameOverImage.SetActive(false);

        stageClearImage.SetActive(false);

        machineGunPossible.SetActive(false);
        missilePossible.SetActive(false);
        laserPossible.SetActive(false);

        continueBtn.onClick.AddListener(OnContinueClickUI);
        optionsBtn.onClick.AddListener(OnOptionsClickUI);
        backBtn.onClick.AddListener(OnBackClickUI);
        exitBtn.onClick.AddListener(OnStartMenuClickUI);
        startMenuBtn.onClick.AddListener(OnStartMenuClickUI);
        nextStageBtn.onClick.AddListener(OnNextStageBtnClickUI);
    }

    public void IncScoreUI (int score)
    {
        scoreText.text = "SCORE : " + score;
    }

    public void OnPauseUI()
    {
        PlayerController.instance.OnPausePlayer();
        pauseImage.SetActive(true);
    }

    public void OnGameOverUI(bool isHighScore)
    {
        if (isHighScore) newHighScoreText.enabled = true;

        resultScoreText.text = "SCORE : " + GameManager.instance.score;
        highscoreText.text = "HIGHSCORE : " + PlayerPrefs.GetInt("Highscore").ToString();

        gameOverImage.SetActive(true);
    }

    public void OnStageClearUI()
    {
        currentScoreText.text = "SCORE : " + GameManager.instance.score;
        stageClearImage.SetActive(true);
    }

    public void OnNextStageBtnClickUI()
    {
        GameManager.instance.LoadNextScene();
    }

    public void OnContinueClickUI()
    {
        GameManager.instance.gameState = GameState.playing;
        pauseImage.SetActive(false);
    }

    public void OnOptionsClickUI()
    {
        volumeImage.SetActive(true);
    }

    public void OnBackClickUI()
    {
        volumeImage.SetActive(false);
    }

    private void OnStartMenuClickUI()
    {
        GameManager.instance.ToStartMenu();
    }

    public void ClassChangePossibleOn(int type)
    {
        if (type == 0)
        {
            machineGunPossible.SetActive(true);
            missilePossible.SetActive(false);
            laserPossible.SetActive(false);
        } else if (type == 1)
        {
            machineGunPossible.SetActive(false);
            missilePossible.SetActive(true);
            laserPossible.SetActive(false);
        } else if (type == 2)
        {
            machineGunPossible.SetActive(false);
            missilePossible.SetActive(false);
            laserPossible.SetActive(true);
        }
    }

    public void ClassChangePossibleOff()
    {
        machineGunPossible.SetActive(false);
        missilePossible.SetActive(false);
        laserPossible.SetActive(false);
    }
}
