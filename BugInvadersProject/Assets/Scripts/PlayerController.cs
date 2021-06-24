using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTypeCount
{
    public int itemType;
    public int itemCount;

    public ItemTypeCount(int type, int count)
    {
        itemType = type;
        itemCount = count;
    }
}

public class PlayerController : MonoBehaviour
{
    public enum MechType
    {
        Gun,
        Missile,
        Laser
    }

    public MechType mechType;
    // 0 = gun, 1 = missile, 2 = laser
    //public GameObject[] bulletPrefabs;
    //private GameObject currBullet;

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

    public ItemTypeCount itemTypeCount;

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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();

        // place holder, should be chosen from start menu in future
        SetMech(PlayerPrefs.GetInt("mechType"));

        UIManager.instance.BombCountUpdateUI(bombCount);

        itemTypeCount = new ItemTypeCount(-1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState != GameState.playing) return;
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

        /*if (changeClass)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && changeClassType == 0)
            {
                SetMech(0);
                UIManager.instance.ClassChangePossibleOff();
                changeClass = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && changeClassType == 1)
            {
                SetMech(1);
                UIManager.instance.ClassChangePossibleOff();
                changeClass = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && changeClassType == 2)
            {
                SetMech(2);
                UIManager.instance.ClassChangePossibleOff();
                changeClass = false;
            }
        }*/
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameState != GameState.playing) return;
        rb.velocity = movement * moveSpeed;
    }

    public void ChangePos(Vector3 pos)
    {
        player.transform.position = pos;
    }

    public void ClassChangeItemGet(int type)
    {
        if (itemTypeCount.itemType == type)
        {
            if (itemTypeCount.itemCount == 3) return;
            itemTypeCount = new ItemTypeCount(type, itemTypeCount.itemCount + 1);
            if (itemTypeCount.itemCount == 3) FullItemStack(type);
        }
        else
        {
            itemTypeCount = new ItemTypeCount(type, 1);
        }
    }

    private void FullItemStack(int type)
    {
        if (type == (int)mechType)
        {
            // Make bullet shoot faster
            fireRate *= 0.75f;

            // Display above change
        }
        else
        {
            // Change class
            SetMech(type);

            itemTypeCount = new ItemTypeCount(-1, 0);
            UIManager.instance.ClassChangeAllOff();
        }
    }

    public void SetMech(int type)
    {
        switch (type)
        {
            case 0:
                mechType = MechType.Gun;
                mechBodies[0].SetActive(true);
                mechBodies[1].SetActive(false);
                mechBodies[2].SetActive(false);
                fireRate = 0.1f;
                break;
            case 1:
                mechType = MechType.Missile;
                mechBodies[0].SetActive(false);
                mechBodies[1].SetActive(true);
                mechBodies[2].SetActive(false);
                fireRate = 0.2f;
                break;
            case 2:
                mechType = MechType.Laser;
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

        bulletPosLeft = new Vector2(playerPos.x - 1.5f, playerPos.y + 2);
        bulletPosRight = new Vector2(playerPos.x + 1.5f, playerPos.y + 2);

        SoundManager.instance.MechShoot((int)mechType);

        BulletManager.instance.FireBullet((int)mechType, bulletPosLeft);
        BulletManager.instance.FireBullet((int)mechType, bulletPosRight);
    }

    private void Bomb()
    {
        if (bombCount == 0) return;
        bombCount--;
        UIManager.instance.BombCountUpdateUI(bombCount);
        Instantiate(bombRingPrefab, playerPos, Quaternion.identity);
    }

    public void TakeDamage()
    {
        StartCoroutine(OnPlayerDeath());
    }

    public IEnumerator OnPlayerDeath()
    {
        // play player death animation & sound
        SoundManager.instance.PlayerDeathSound();
        SoundManager.instance.MakeExplosion(playerPos);
        player.SetActive(false);
        GameManager.instance.gameState = GameState.gameOver;

        yield return new WaitForSeconds(1);

        GameManager.instance.OnGameOver();
    }

    public void OnPausePlayer()
    {
        rb.velocity = Vector2.zero;
    }
}
