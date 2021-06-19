using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyBulletPrefab;

    public EnemyType enemyType;

    [SerializeField] int hp;
    [SerializeField] float bugSpeed;
    [SerializeField] int score;
    [SerializeField] bool doShoot;

    [SerializeField] float fireRate;

    private bool canShoot = false;
    private float currTime = 0;
    private bool startTime = false;

    private GameObject target;
    private Vector3 direction;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float BugSpeed
    {
        get { return bugSpeed; }
        set { bugSpeed = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public bool DoShoot
    {
        get { return doShoot; }
        set { doShoot = value; }
    }
    public bool CanShoot
    {
        get { return canShoot; }
        set { canShoot = value; }
    }

    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }
    public float CurrTime
    {
        get { return currTime; }
        set { currTime = value; }
    }
    public bool StartTime
    {
        get { return startTime; }
        set { startTime = value; }
    }

    public GameObject Target
    {
        get { return target; }
    }

    public virtual void Initialize(GameObject targetPlayer)
    {
        target = targetPlayer;
        direction = (target.transform.position - transform.position).normalized;

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public virtual void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            SoundManager.instance.AlienDeathSound();
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameManager.instance.IncScore(this.Score);
            this.gameObject.SetActive(false);
        }
    }

    public virtual void Update()
    {
        if (GameManager.instance.gameState != GameState.playing) return;
        transform.position += direction * BugSpeed * Time.deltaTime;

        if (startTime) currTime += Time.deltaTime;

        if (doShoot && canShoot) { Debug.Log(GameManager.instance.gameState); Fire(); }
    }

    public virtual void Fire()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.instance.TakeDamage();
        }
        else if (collision.tag == "DetectionWall")
        {
            this.gameObject.SetActive(false);
        }
        else if (collision.tag == "BugShootTrigger")
        {
            startTime = true;
        }
        else if (collision.tag == "BugStopShootTrigger")
        {
            startTime = false;
        }
    }
}
