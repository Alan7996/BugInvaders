using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int dmg = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
    }
}
