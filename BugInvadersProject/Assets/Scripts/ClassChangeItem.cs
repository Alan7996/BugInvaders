using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassChangeItem : MonoBehaviour
{
    public GameObject machineGunCircle;
    public GameObject missleCircle;
    public GameObject laserCircle;

    public int mechType = 0;

    public int travelSpeed = 1;
    private Vector3 direction;

    public void Initialize(int i, Vector3 direct)
    {
        mechType = i;
        direction = direct;

        if (i == 0)
        {
            machineGunCircle.SetActive(true);
            missleCircle.SetActive(false);
            laserCircle.SetActive(false);
        } else if (i == 1)
        {
            machineGunCircle.SetActive(false);
            missleCircle.SetActive(true);
            laserCircle.SetActive(false);
        } else if (i == 2)
        {
            machineGunCircle.SetActive(false);
            missleCircle.SetActive(false);
            laserCircle.SetActive(true);
        }
    }

    private void Update()
    {
        transform.position += direction * travelSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!collision.GetComponentInParent<PlayerController>().DuplicateClass(mechType))
            {
                collision.GetComponentInParent<PlayerController>().ClassChangeItemGet(mechType);
                UIManager.instance.ClassChangePossibleOn(mechType);
            }
            Destroy(this.gameObject);
        }
    }
}
