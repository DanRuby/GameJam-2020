using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Lever : MonoBehaviour, IHittable
{
    [SerializeField] AudioClip turnOnSound;
    [SerializeField] Activate[] activateObjects;

    AudioSource audioSource;
    Animator animator;
    BoxCollider2D collider2D;

    public void Hit(Transform player)
    {
        animator.enabled = true;
        StartCoroutine(ActivateGameObject());
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    IEnumerator ActivateGameObject()
    {
        yield return new WaitForSeconds(.4f);
        foreach(Activate activateObject in activateObjects)
            activateObject.ActivateComponent();
        audioSource.PlayOneShot(turnOnSound);
        collider2D.enabled = false;
        this.enabled = false;
    }
}
