using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectMech : MonoBehaviour
{
    public Button mechGunBtn;
    public Button mechMissileBtn;
    public Button mechLaserBtn;
    public Button backBtn;

    // Start is called before the first frame update
    void Start()
    {
        mechGunBtn.onClick.AddListener(SelectMechGun);
        mechMissileBtn.onClick.AddListener(SelectMechMissile);
        mechLaserBtn.onClick.AddListener(SelectMechLaser);
        backBtn.onClick.AddListener(BackToStart);
    }

    void SelectMechGun()
    {
        PlayerPrefs.SetInt("mechType", 0);
        SceneManager.LoadScene(2);
    }

    void SelectMechMissile()
    {
        PlayerPrefs.SetInt("mechType", 1);
        SceneManager.LoadScene(2);
    }

    void SelectMechLaser()
    {
        PlayerPrefs.SetInt("mechType", 2);
        SceneManager.LoadScene(2);
    }

    void BackToStart()
    {
        SceneManager.LoadScene(0);
    }
}
