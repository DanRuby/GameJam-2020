using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [SerializeField] float movespeed;
    [SerializeField] bool movesInOneDirection;

    PlayerMovement playerMovement;
    Vector3 startPosition;
    Vector3 currentVelocity = new Vector3(0, 0, 0);

    void OnEnable()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        currentVelocity = (destination - startPosition).normalized * movespeed * Time.fixedDeltaTime;
        transform.Translate(currentVelocity);
        if ((destination - transform.position).magnitude <= .1f)
        {
            if (movesInOneDirection)
            {
                currentVelocity = Vector3.zero;
                this.enabled = false;
            }
            Vector3 temporary = startPosition;
            startPosition = destination;
            destination = temporary;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var temporary = collision.gameObject.GetComponent<PlayerMovement>();
        if (temporary != null)
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
            playerMovement.AddVelocity(currentVelocity / Time.fixedDeltaTime);
        }
    }
}
