using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public Button startBtn;
    public Button optionsBtn;
    public Button exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        optionsBtn.onClick.AddListener(Options);
        exitBtn.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Options()
    {
        Debug.Log("Options clicked");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
