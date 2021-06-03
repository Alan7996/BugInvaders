using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBugController : MonoBehaviour
{
    private int score = 100;
    private int hP;
    private float bugSpeed = 10f;
    public GameObject target;
    private Vector3 direction;

    public void Initialize(GameObject targetPlayer)
    {
        hP = 2;
        target = targetPlayer;
        direction = (target.transform.position - transform.position).normalized;

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * bugSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            hP -= collision.gameObject.GetComponent<BulletController>().dmg;
            Destroy(collision.gameObject);
        } else if (collision.tag == "Bomb")
        {
            hP -= 4;
        } else if (collision.tag == "Player")
        {
            Destroy(collision.gameObject);
        }

        if (hP <= 0)
        {
            GameManager.instance.IncScore(score);
            Destroy(this.gameObject);
        }
    }
}
