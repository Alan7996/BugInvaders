using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MechType
{
    Gun,
    Missile,
    Laser
}

public class PlayerController : MonoBehaviour
{
    private MechType currMechType;

    // 0 = gun, 1 = missile, 2 = laser
    public GameObject[] bulletPrefabs;
    private GameObject currBullet;

    Vector2 bulletPosLeft;
    Vector2 bulletPosRight;

    public GameObject bombRingPrefab;

    // 0 = head, 1 = body, 2 = left arm, 3 = right arm
    public GameObject[] mechBodyParts;
    private SpriteRenderer mechHeadRenderer;
    private SpriteRenderer mechBodyRenderer;
    private SpriteRenderer mechLeftRenderer;
    private SpriteRenderer mechRightRenderer;
    public Sprite[] mechSprites;

    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 playerPos;

    private int bombCount = 3;

    private Vector2 movement;
    public float moveSpeed = 15f;

    private float fireRate = 0.1f;
    private float currTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        mechHeadRenderer = mechBodyParts[0].GetComponent<SpriteRenderer>();
        mechBodyRenderer = mechBodyParts[1].GetComponent<SpriteRenderer>();
        mechLeftRenderer = mechBodyParts[2].GetComponent<SpriteRenderer>();
        mechRightRenderer = mechBodyParts[3].GetComponent<SpriteRenderer>();

        // place holder, should be chosen from start menu in future
        SetMech(0);
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
                currMechType = MechType.Gun;
                currBullet = bulletPrefabs[0];
                mechHeadRenderer.sprite = mechSprites[0];
                mechBodyRenderer.sprite = mechSprites[1];
                mechLeftRenderer.sprite = mechSprites[2];
                mechRightRenderer.sprite = mechSprites[3];
                fireRate = 0.1f;
                break;
            case 1:
                currMechType = MechType.Missile;
                currBullet = bulletPrefabs[1];
                mechHeadRenderer.sprite = mechSprites[4];
                mechBodyRenderer.sprite = mechSprites[5];
                mechLeftRenderer.sprite = mechSprites[6];
                mechRightRenderer.sprite = mechSprites[7];
                fireRate = 0.2f;
                break;
            case 2:
                currMechType = MechType.Laser;
                currBullet = bulletPrefabs[2];
                mechHeadRenderer.sprite = mechSprites[8];
                mechBodyRenderer.sprite = mechSprites[9];
                mechLeftRenderer.sprite = mechSprites[10];
                mechRightRenderer.sprite = mechSprites[11];
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
            SetMech((int)currMechType);
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
}
