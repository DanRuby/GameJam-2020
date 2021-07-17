using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject energyBallPrefab;
    [SerializeField] float shotsPerSecond;

    bool canShoot = true;

    void Update()
    {
        if (canShoot)
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(1 / shotsPerSecond);
        Instantiate(energyBallPrefab, transform.position + transform.right * .5f,transform.rotation);
        canShoot = true;
    }
}
