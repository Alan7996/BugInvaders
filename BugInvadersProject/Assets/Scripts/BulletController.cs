using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum BulletType
    {
        Gun,
        Missile,
        Laser
    }

    public BulletType bulletType;

    bool isHit = false;

    public bool IsHit
    {
        get
        {
            return isHit;
        }
    }

    public float bulletSpeed = 10f;
    public int dmg = 1;

    void Update()
    {
        transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<BodyBugController>().TakeDamage(dmg);
            this.gameObject.SetActive(false);
            isHit = true;
        } else if (collision.tag == "DetectionWall")
        {
            this.gameObject.SetActive(false);
            isHit = true;
        }
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
        this.isHit = false;
    }
}
