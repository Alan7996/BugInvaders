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

    public Button mechGunBtn;
    public Button mechMissileBtn;
    public Button mechLaserBtn;
    public Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        optionsBtn.onClick.AddListener(Options);
        exitBtn.onClick.AddListener(ExitGame);

        mechGunBtn.onClick.AddListener(SelectMechGun);
        mechMissileBtn.onClick.AddListener(SelectMechMissile);
        mechLaserBtn.onClick.AddListener(SelectMechLaser);
        backBtn.onClick.AddListener(BackToStart);
    }

    void StartGame()
    {
        startBtn.gameObject.SetActive(false);
        optionsBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);

        mechGunBtn.gameObject.SetActive(true);
        mechMissileBtn.gameObject.SetActive(true);
        mechLaserBtn.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(true);
    }

    void Options()
    {
        Debug.Log("Options clicked");
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void SelectMechGun()
    {
        PlayerPrefs.SetInt("mechType", 0);
        SceneManager.LoadScene(1);
    }

    void SelectMechMissile()
    {
        PlayerPrefs.SetInt("mechType", 1);
        SceneManager.LoadScene(1);
    }

    void SelectMechLaser()
    {
        PlayerPrefs.SetInt("mechType", 2);
        SceneManager.LoadScene(1);
    }

    void BackToStart()
    {
        startBtn.gameObject.SetActive(true);
        optionsBtn.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);

        mechGunBtn.gameObject.SetActive(false);
        mechMissileBtn.gameObject.SetActive(false);
        mechLaserBtn.gameObject.SetActive(false);
        backBtn.gameObject.SetActive(false);
    }
}
