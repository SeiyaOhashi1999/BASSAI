using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    Player player;
    bool isGround;

    private void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter:" + collision);
        if (collision.tag != "Spike")
        {
            player.isGround = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay:" + collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit:" + collision);
        if(collision.tag != "Spike")
        {
            player.isGround = false;
        }
        
    }
}
