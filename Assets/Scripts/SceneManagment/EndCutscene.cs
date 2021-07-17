using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    [SerializeField]PlayerMovement playerMovement;
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Animator animator;
    [SerializeField] Light2D playerLight;
    [SerializeField] AudioClip lightSound;
    [SerializeField] AudioSource backgroundMusic;

    AudioSource audioSource;
    Light2D light;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Cutscene(collision));
    }

    IEnumerator Cutscene(Collider2D collision)
    {
        backgroundMusic.enabled = false;
        playerMovement.SetVelocity(new Vector3(0, 0, 0));
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(lightSound);
        light.enabled = false;
        yield return new WaitForSeconds(1f);
        collision.gameObject.transform.Rotate(0, 180, 0);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(lightSound);
        playerLight.intensity=0;
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
