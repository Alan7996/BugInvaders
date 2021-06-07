using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Transform bottomDetectionWall;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -71.8f)
        {
            transform.position = new Vector2(0, 71.8f);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector2(0, -1 * Time.deltaTime * speed));
    }
}
