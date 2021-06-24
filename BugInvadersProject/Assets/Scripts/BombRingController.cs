using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRingController : MonoBehaviour
{
    private Rigidbody2D rb;

    public int bombDmg = 4;

    [SerializeField]
    private float torque = 60;

    private Vector3 incScale;
    private Vector2 maxScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.AddTorque(torque);

        incScale = new Vector3(0.1f, 0.1f);
        maxScale = new Vector2(29f, 29f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += incScale;

        if (transform.localScale.x > maxScale.x && transform.localScale.y > maxScale.y) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bombDmg);
        } else if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
