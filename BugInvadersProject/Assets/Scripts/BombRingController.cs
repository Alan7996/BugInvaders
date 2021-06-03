using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRingController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float torque = 60;

    private Vector3 incScale;
    private Vector2 maxScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.AddTorque(torque);

        incScale = new Vector3(0.05f, 0.05f);
        maxScale = new Vector2(35f, 35f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += incScale;

        if (transform.localScale.x > maxScale.x && transform.localScale.y > maxScale.y) Destroy(this.gameObject);
    }
}
