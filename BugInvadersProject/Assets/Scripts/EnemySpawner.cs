using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;

    public GameObject bodyBug1Prefab;

    // total 5 spawn points
    public GameObject[] spawnPoints;

    private float windowWidth = 15;

    private float spawnTime;
    private float spawnMaxTime = 5f;
    private float spawnMinTime = 2f;

    public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime = Random.Range(spawnMaxTime, spawnMinTime);
    }

    IEnumerator waitSpawner()
    {
        yield
            return new WaitForSeconds(spawnTime);

        while (!stop)
        {
            BodyBugController enemy = Object.Instantiate(bodyBug1Prefab, spawnPoints[Random.Range(0,5)].transform.position, Quaternion.identity).GetComponent<BodyBugController>();
            enemy.Initialize(player);

            yield
                return new WaitForSeconds(spawnTime);
        }
    }
}
