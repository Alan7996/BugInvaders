using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType<SoundManager>();
            }
            return s_instance;
        }
    }

    private static SoundManager s_instance;

    private AudioSource audio;

    public AudioClip mechGunClip;
    public AudioClip mechMissileClip;
    public AudioClip mechLaserClip;

    public AudioClip[] bgm;

    public AudioClip alienDeathClip;
    public AudioClip playerExplosionClip;

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = bgm[SceneManager.GetActiveScene().buildIndex];
        audio.loop = true;
        audio.Play();
    }

    public void MechShoot(int mechType)
    {
        if (mechType == 0)
        {
            audio.PlayOneShot(mechGunClip);
        } else if (mechType == 1)
        {
            audio.PlayOneShot(mechMissileClip);
        } else if (mechType == 2)
        {
            audio.PlayOneShot(mechLaserClip);
        }
    }

    public void AlienDeathSound()
    {
        audio.PlayOneShot(alienDeathClip);
    }

    public void PlayerDeathSound()
    {
        audio.PlayOneShot(playerExplosionClip);
    }

    public void MakeExplosion(Vector3 position)
    {
        GameObject explodey = Instantiate(explosionPrefab, position, Quaternion.identity, transform);
        Destroy(explodey, 25.0f);
    }
}
