using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

[RequireComponent(typeof(Light2D))]

public class ActivateLight : Activate
{
    Light2D light;

    private void OnEnable()
    {
        light = GetComponent<Light2D>();
    }

    override public void ActivateComponent()
    {
        light.enabled = true;
    }
    
}
