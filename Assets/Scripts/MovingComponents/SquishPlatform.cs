using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishPlatform : MovingPlatform
{
    PlayerMovement playerMovement;

    private  void OnCollisionEnter2D(Collision2D collision)
    {
        playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerMovement != null)
        {
            if (playerMovement.IsGrounded && collision.GetContact(0).normal.y == 1)
                SceneLoader.Instance.ReloadCurrentLevel();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerMovement = null;
    }
}
