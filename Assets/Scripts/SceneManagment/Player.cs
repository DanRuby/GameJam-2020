using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IEnergisable
{
   
    public void Energise(GameObject energyball)
    {
        Destroy(energyball);
        SceneLoader.Instance.ReloadCurrentLevel();
    }

    public void Respawn()
    {
        SceneLoader.Instance.ReloadCurrentLevel();
    }

}
