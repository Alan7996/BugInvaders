using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : Bullet
{
    public void Initialize(Transform targetPlayer)
    {
        //Target = targetPlayer;
        Direction = (targetPlayer.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.instance.TakeDamage();
            Destroy(this.gameObject);
        }
        else if (collision.tag == "DetectionWall")
        {
            Destroy(this.gameObject);
        }
    }
}
