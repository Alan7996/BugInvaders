using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum MechType
{
    Gun,
    Missile,
    Laser
}*/

public class PlayerController : MonoBehaviour
{
    // 0 = gun, 1 = missile, 2 = laser
    public GameObject[] bulletPrefabs;
    private GameObject currBullet;

    Vector2 bulletPosLeft;
    Vector2 bulletPosRight;

    public GameObject bombRingPrefab;

    // 0 = gun, 1 = missile, 2 = laser
    public GameObject[] mechBodies;

    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 playerPos;

    private int bombCount = 3;

    private Vector2 movement;
    public float moveSpeed = 15f;

    private float fireRate = 0.1f;
    private float currTime = 0f;

    public static PlayerController instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PlayerController>();
            }
            return m_instance;
        }
    }

    private static PlayerController m_instance;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();

        // place holder, should be chosen from start menu in future
        SetMech(PlayerPrefs.GetInt("mechType"));
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerPos = player.transform.position;

        currTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E))
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Bomb();
        }

        // should become a separate skill later on
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetMech(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetMech(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetMech(2);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    public void SetMech(int type)
    {
        switch (type)
        {
            case 0:
                currBullet = bulletPrefabs[0];
                mechBodies[0].SetActive(true);
                mechBodies[1].SetActive(false);
                mechBodies[2].SetActive(false);
                fireRate = 0.1f;
                break;
            case 1:
                currBullet = bulletPrefabs[1];
                mechBodies[0].SetActive(false);
                mechBodies[1].SetActive(true);
                mechBodies[2].SetActive(false);
                fireRate = 0.2f;
                break;
            case 2:
                currBullet = bulletPrefabs[2];
                mechBodies[0].SetActive(false);
                mechBodies[1].SetActive(false);
                mechBodies[2].SetActive(true);
                fireRate = 0.3f;
                break;
            default:
                Debug.Log("Impossible mech");
                break;
        }
    }

    private void Fire()
    {
        if (currTime < fireRate) return;
        currTime = 0;

        if (!currBullet)
        {
            SetMech(PlayerPrefs.GetInt("mechType"));
        }

        bulletPosLeft = new Vector2(playerPos.x - 1.5f, playerPos.y + 2);
        bulletPosRight = new Vector2(playerPos.x + 1.5f, playerPos.y + 2);
        
        Instantiate(currBullet, bulletPosLeft, Quaternion.identity);
        Instantiate(currBullet, bulletPosRight, Quaternion.identity);
    }

    private void Bomb()
    {
        if (bombCount == 0) return;
        bombCount--;
        Instantiate(bombRingPrefab, playerPos, Quaternion.identity);
    }

    public void TakeDamage()
    {
        GameManager.instance.OnGameOver();
        //Destroy(this.gameObject);
    }
}
