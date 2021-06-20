using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    body1, body2, shoot1, shoot2, big1, big2
}

[System.Serializable]
public class EnemySpawnClass
{
    public Enemy enemyType;
    public int enemyCount;

    public EnemySpawnClass(Enemy enemy, int count)
    {
        enemyType = enemy;
        enemyCount = count;
    }
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance
    {
        get
        {
            if (e_instance == null)
            {
                e_instance = FindObjectOfType<EnemySpawner>();
            }
            return e_instance;
        }
    }

    private static EnemySpawner e_instance;

    public GameObject player;

    [SerializeField] EnemySpawnClass[] enemySpawnInfo;

    [SerializeField] bool bossEncounter;

    List<Enemy>[] enemyReady = new List<Enemy>[6];

    List<Enemy> bodyBug1Ready = new List<Enemy>();
    List<Enemy> bodyBug2Ready = new List<Enemy>();
    List<Enemy> shootBug1Ready = new List<Enemy>();
    List<Enemy> shootBug2Ready = new List<Enemy>();
    List<Enemy> bigBug1Ready = new List<Enemy>();
    List<Enemy> bigBug2Ready = new List<Enemy>();

    private List<int> enemyTypeList = new List<int>();

    // total 5 spawn points
    public GameObject[] spawnPoints;

    private float spawnTime;
    private float spawnMaxTime = 2f;
    private float spawnMinTime = 0.5f;

    public bool stop = false;

    private void Awake()
    {
        enemyReady[0] = bodyBug1Ready;
        enemyReady[1] = bodyBug2Ready;
        enemyReady[2] = shootBug1Ready;
        enemyReady[3] = shootBug2Ready;
        enemyReady[4] = bigBug1Ready;
        enemyReady[5] = bigBug2Ready;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < enemySpawnInfo[i].enemyCount; j++)
            {
                var bug = Instantiate(enemySpawnInfo[i].enemyType);
                enemyReady[i].Add(bug);
                bug.transform.position = new Vector3(1000, -1000, -1000);
            }
        }
    }

    public void SpawnEnemy()
    {
        if (enemyTypeList.Count == 0) return;
        int enemyTypeNum = Random.Range(0, enemyTypeList.Count);
        int spawnEnemy = enemyTypeList[enemyTypeNum];
        var enemy = enemyReady[spawnEnemy][0];
        enemyReady[spawnEnemy].RemoveAt(0);
        if (enemyReady[spawnEnemy].Count == 0) enemyTypeList.RemoveAt(enemyTypeNum);

        enemy.transform.position = spawnPoints[Random.Range(0, 9)].transform.position;
        enemy.Initialize(player);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());

        for (int i = 0; i < 6; i++)
        {
            if (enemySpawnInfo[i].enemyCount != 0)
            {
                enemyTypeList.Add(i);
            }
        }

        spawnTime = Random.Range(spawnMaxTime, spawnMinTime);
    }

    IEnumerator waitSpawner()
    {
        yield
            return new WaitForSeconds(spawnTime);

        while (!stop)
        {
            SpawnEnemy();

            yield
                return new WaitForSeconds(spawnTime);

            spawnTime = Random.Range(spawnMaxTime, spawnMinTime);
        }
    }

    public void StopCoroutine()
    {
        StopAllCoroutines();
    }
}
