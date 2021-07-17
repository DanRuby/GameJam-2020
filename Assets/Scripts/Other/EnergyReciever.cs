using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnergyReciever : MonoBehaviour,IEnergisable
{
    [SerializeField] AudioClip turnOnSound;
    [SerializeField] Activate[] activateObjects;
    [SerializeField] bool turningSourceOff;
    [SerializeField] int neededEnergy;

    AudioSource audioSource;
    Light2D light;
    int currentEnergy;
    Animator animator;

    public void Energise(GameObject energyball)
    {
        currentEnergy++;
        if (turningSourceOff)
        {
            energyball.GetComponent<EnergyBall>().ParentTorch.TurnOff();
        }
        if (currentEnergy >= neededEnergy)
        {
            light.intensity = 1;
            Activate();
            animator.enabled = true;
        }
        Destroy(energyball);
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light2D>();
        light.intensity = .2f;
        animator = GetComponent<Animator>();
        animator.enabled = false;
        currentEnergy = 0;
    }

    void Activate()
    {
        StartCoroutine(ActivateGameObject());
    }

    IEnumerator ActivateGameObject()
    {
        yield return new WaitForSeconds(.5f);
        foreach (Activate activateObject in activateObjects)
            activateObject.ActivateComponent();
        audioSource.PlayOneShot(turnOnSound);
    }

}
