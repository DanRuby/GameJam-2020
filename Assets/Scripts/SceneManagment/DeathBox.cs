using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       player=collision.GetComponent<Player>();
        if (player != null)
            player.Respawn();
        else Destroy(collision.gameObject);

    }
}
