using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyBulletPrefab;

    public EnemyType enemyType;

    private SpriteRenderer sprite;

    [SerializeField] int hp;
    [SerializeField] float bugSpeed;
    [SerializeField] int score;
    [SerializeField] bool doShoot;

    [SerializeField] float fireRate;

    private float blinkTime = 0.1f;

    private bool canShoot = false;
    private float totalTime = 0;
    private float currTime = 0;
    private bool startTime = false;

    private GameObject target;
    private Vector3 direction;

    [SerializeField] float dropChance;

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
        totalTime = 0;
        currTime = 0;

        target = targetPlayer;
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        direction = (target.transform.position - transform.position).normalized;

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public virtual void TakeDamage(int dmg)
    {
        if (hp <= 0) return;
        hp -= dmg;
        StartCoroutine(BlinkRed());
        if (hp <= 0)
        {
            if (Random.Range(0f, 1f) < dropChance)
            {
                ItemDropper.instance.DropClassChangeItem(transform.position, direction);
            }

            SoundManager.instance.AlienDeathSound();
            SoundManager.instance.MakeExplosion(transform.position);

            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            GameManager.instance.IncScore(this.Score);
            EnemySpawner.instance.DecTotalEnemyNum();

            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator BlinkRed()
    {
        sprite.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(blinkTime);
        sprite.color = new Color(255, 255, 255);
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        if (GameManager.instance.gameState != GameState.playing) return;
        transform.position += direction * BugSpeed * Time.deltaTime;

        if (startTime) { currTime += Time.deltaTime; totalTime += Time.deltaTime; }

        if (doShoot && canShoot) { Fire(); }
        if (totalTime > 2) { totalTime = 0; LookAtTarget(); }
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
            EnemySpawner.instance.DecTotalEnemyNum();
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
