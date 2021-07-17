using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackRange=.39f;
    [SerializeField] Transform hitbox;
    [SerializeField] float normalCooldown=.8f;

    Animator animator;
    float nextAttackTime;
    float currentTime;
   
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        currentTime = Time.time;
        if (currentTime>=nextAttackTime)
        {
            //Mousebtn 0 joyubutton 2(x)
            if (Input.GetButtonDown("NormalAttack"))
                NormalAttack();
        }
    }

    void NormalAttack()
    {
        nextAttackTime = currentTime + normalCooldown;
        animator.SetTrigger("NormalAttack");
        Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(hitbox.position, attackRange);
        foreach(Collider2D enemy in hitEnemies)
        {
            var hittable=enemy.GetComponent<IHittable>();
            if (hittable != null)
                hittable.Hit(transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitbox == null)
            return;
        Gizmos.DrawWireSphere(hitbox.position, attackRange);
    }
}
