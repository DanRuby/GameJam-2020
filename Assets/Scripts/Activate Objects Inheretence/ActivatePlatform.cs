using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingPlatform))]

public class ActivatePlatform :Activate
{
    MovingPlatform movingPlatform;

    private void OnEnable()
    {
        movingPlatform = GetComponent<MovingPlatform>();
    }

    public override void ActivateComponent()
    {
        movingPlatform.enabled = true;
    }
}
