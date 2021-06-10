using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBugController : MonoBehaviour
{
    private int score = 100;
    private int hP;
    private float bugSpeed = 10f;
    public Transform target;
    private Vector3 direction;
    private Vector3 tempPlace;

    public void Initialize(Transform targetPlayer)
    {
        hP = 2;
        target = targetPlayer;
        direction = (target.position - transform.position).normalized;

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    private void Start()
    {
        tempPlace = new Vector3(-100, -100, 0);
    }

    void Update()
    {
        transform.position += direction * bugSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.instance.TakeDamage();
        } else if (collision.tag == "DetectionWall")
        {
            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int dmg)
    {
        hP -= dmg;
        if (hP <= 0)
        {
            SoundManager.instance.AlienDeathSound();
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameManager.instance.IncScore(score);
            this.gameObject.SetActive(false);
        }
    }
}
