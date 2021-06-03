using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;

    public GameObject bodyBug1Prefab;

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
            Vector3 spawnPosition = new Vector3(Random.Range(-windowWidth/2, windowWidth/2), 0, 0);

            BodyBugController enemy = Object.Instantiate(bodyBug1Prefab, spawnPosition + transform.position, Quaternion.identity).GetComponent<BodyBugController>();
            enemy.Initialize(player);

            yield
                return new WaitForSeconds(spawnTime);
        }
    }
}
