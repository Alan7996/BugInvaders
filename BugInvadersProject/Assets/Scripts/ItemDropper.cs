using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public static ItemDropper instance
    {
        get
        {
            if (i_instance == null)
            {
                i_instance = FindObjectOfType<ItemDropper>();
            }
            return i_instance;
        }
    }

    private static ItemDropper i_instance;

    public GameObject classChangeItemPrefab;

    public void DropClassChangeItem(Vector2 pos, Vector3 direction)
    {
        GameObject item = Instantiate(classChangeItemPrefab, pos, Quaternion.identity, transform);
        item.GetComponent<ClassChangeItem>().Initialize(Random.Range(0, 3), direction);
    }
}
