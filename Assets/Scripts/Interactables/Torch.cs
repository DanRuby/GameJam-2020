using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class Torch : MonoBehaviour, IHittable
{
    [SerializeField] GameObject fireBallPrefab;

    BoxCollider2D collider;
    GameObject energyBall=null;
    Light2D light;
    Animator animator;

    public void Hit(Transform player)
    {
        if (energyBall == null)
        {
           energyBall=Instantiate(fireBallPrefab, transform.position + player.right * .5f, player.rotation);
            energyBall.GetComponent<EnergyBall>().SetTorch(this);
        }
    }

    private void OnDisable()
    {
        animator.SetTrigger("TurnOff");
        light.intensity=.2f;
    }

    private void OnEnable()
    {
        collider = GetComponent<BoxCollider2D>();
        light = GetComponent<Light2D>();
        animator = GetComponent<Animator>();
    }

    public void TurnOff()
    {
        collider.enabled = false;
        this.enabled = false;
    }
}
