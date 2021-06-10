using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : Bullet
{
    private void Awake()
    {
        Direction = Vector3.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(dmg);
            gameObject.SetActive(false);
            IsHit = true;
        }
        else if (collision.tag == "DetectionWall")
        {
            gameObject.SetActive(false);
            IsHit = true;
        }
    }
}
