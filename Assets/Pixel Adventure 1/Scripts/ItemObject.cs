using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            item_collector.instance.AddItem(item);
          // PlayerLife.instance.Heal(30);
            Destroy(gameObject);
        }
    }
}
