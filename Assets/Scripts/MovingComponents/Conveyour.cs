using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyour : MonoBehaviour
{
    [SerializeField] float velocity;

    PlayerMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var temporary = collision.gameObject.GetComponent<PlayerMovement>();
        if (temporary!=null)
            playerMovement = temporary;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            playerMovement = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerMovement != null)
        {
            
            playerMovement.AddVelocity(new Vector3(velocity, 0, 0));
        }
        
    }
}
