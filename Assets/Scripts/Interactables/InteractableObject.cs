using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class InteractableObject : MonoBehaviour
{
    [SerializeField] GameObject MessageScreen;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAttack playerAttack;

    bool inRange;
    bool showsMessage;

    private void OnEnable()
    {
        inRange = false;
    }

    void Update()
    {
        if (inRange)
            if (Input.GetKeyDown(KeyCode.F))
            {
                StopPlayerAndShowMessage();
            }
    }

    private void StopPlayerAndShowMessage()
    {
        playerAttack.enabled = showsMessage;
        playerMovement.SetVelocity(new Vector3(0, 0, 0));
        playerMovement.enabled = showsMessage;
        showsMessage = !showsMessage;
        MessageScreen.SetActive(showsMessage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
