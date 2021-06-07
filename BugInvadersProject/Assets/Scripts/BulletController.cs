using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
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
        }
    }
}
