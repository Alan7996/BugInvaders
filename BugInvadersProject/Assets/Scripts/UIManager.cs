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

    public TMP_Text bombCountText;

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

    public GameObject[] machineGunPossible;
    public GameObject[] missilePossible;
    public GameObject[] laserPossible;

    // Start is called before the first frame update
    void Start()
    {
        pauseImage.SetActive(false);
        volumeImage.SetActive(false);

        newHighScoreText.enabled = false;
        gameOverImage.SetActive(false);

        stageClearImage.SetActive(false);

        ClassChangeAllOff();

        continueBtn.onClick.AddListener(OnContinueClickUI);
        optionsBtn.onClick.AddListener(OnOptionsClickUI);
        backBtn.onClick.AddListener(OnBackClickUI);
        exitBtn.onClick.AddListener(OnStartMenuClickUI);
        startMenuBtn.onClick.AddListener(OnStartMenuClickUI);
        nextStageBtn.onClick.AddListener(OnNextStageBtnClickUI);

        BombCountUpdateUI();
        DisplayCurrentItems();
    }

    private void BombCountUpdateUI()
    {
        bombCountText.text = "x " + PlayerController.instance.bombCount;
    }

    public void BombCountUpdateUI (int count)
    {
        bombCountText.text = "x " + count;
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

    public void ClassChangeAllOff()
    {
        foreach (GameObject x in machineGunPossible)
        {
            x.SetActive(false);
        }
        foreach (GameObject x in missilePossible)
        {
            x.SetActive(false);
        }
        foreach (GameObject x in laserPossible)
        {
            x.SetActive(false);
        }
    }

    public void ClassChangePossibleOn(int type, int num)
    {
        if (num == 0) return;
        if (num == 1) ClassChangeAllOff();
        if (type == 0)
        {
            machineGunPossible[num - 1].SetActive(true);
        } else if (type == 1)
        {
            missilePossible[num - 1].SetActive(true);
        } else if (type == 2)
        {
            laserPossible[num - 1].SetActive(true);
        }
    }

    private void DisplayCurrentItems()
    {
        int type = PlayerController.instance.itemTypeCount.itemType;
        if (type == -1) return;

        int num = PlayerController.instance.itemTypeCount.itemCount;
        if (type == 0)
        {
            for (int i = 0; i < num; i++)
            {
                machineGunPossible[i].SetActive(true);
            }
        } else if (type == 1)
        {
            for (int i = 0; i < num; i++)
            {
                missilePossible[i].SetActive(true);
            }
        } else if (type == 2)
        {
            for (int i = 0; i < num; i++)
            {
                laserPossible[i].SetActive(true);
            }
        }
    }
}
