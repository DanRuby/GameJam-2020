using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyReflector : MonoBehaviour, IEnergisable
{
    ContactPoint2D contactPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        contactPoint = collision.GetContact(0);
    }

    public void Energise(GameObject energyball)
    {
        StartCoroutine(WaitForFixedUpdate(energyball));  
    }

    IEnumerator WaitForFixedUpdate(GameObject energyball)
    {
        yield return new WaitForFixedUpdate();
        energyball.transform.right = Vector3.Reflect(energyball.transform.right, contactPoint.normal);
    }
    

}
