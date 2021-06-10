using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Gun,
        Missile,
        Laser,
        Enemy
    }

    public BulletType bulletType;

    bool isHit = false;

    public bool IsHit
    {
        get { return isHit; }
        set { isHit = value; }
    }

    public float bulletSpeed = 10f;
    public int dmg = 1;

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    /*private Transform target;
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }*/

    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
        this.isHit = false;
    }
}
