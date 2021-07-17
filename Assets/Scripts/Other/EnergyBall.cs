using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] float movespeed;

    public Torch ParentTorch { get; private set; }
    IEnergisable energisable;
    
    void FixedUpdate()
    {
       transform.Translate(transform.right * movespeed * Time.fixedDeltaTime,Space.World);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        energisable = collision.transform.GetComponent<IEnergisable>();
        if (energisable != null)
            energisable.Energise(gameObject);
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        energisable=collision.GetComponent<IEnergisable>();
        if (energisable != null)
            energisable.Energise(gameObject);
    }

    public void SetTorch(Torch torch)
    {
        ParentTorch = torch;
    }
}
