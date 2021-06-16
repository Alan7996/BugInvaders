using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : Bullet
{
    public void Initialize(Transform targetPlayer)
    {
        Direction = (targetPlayer.position - transform.position).normalized;
    }

    public void Initialize(Transform targetPlayer, bool left)
    {
        int degree = 10;
        if (left) degree *= -1;
        Direction = Quaternion.Euler(0, 0, degree) * (targetPlayer.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DetectionWall")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.instance.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
