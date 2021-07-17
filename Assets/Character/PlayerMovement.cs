using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Main Movement")]
    [SerializeField] float horizontalVelocity=5f;
    [SerializeField] float gravityAcceleration=2.5f;
    [SerializeField] float jumpAcceleration=8f;
    [SerializeField] int maxJumpFrames=12;
    [SerializeField] AudioClip jumpSound;

    AudioSource audioSource;
    Rigidbody2D rigidbody;
    Animator animator;
    CapsuleCollider2D collider;
    LayerMask platformMask;
    bool isGrounded;
    float maxGravityVelocity=10;//Максимальная скорость падения

    public bool IsGrounded=>isGrounded;//Персонаж на земле

    Vector3 playerVelocity = new Vector3(0, 0);
    float oldDirection;//Старое направление движения
    float horizontalAxis;//Инпут по горизонтальной оси
  
    int jumpFrames;//Сколько фреймов игрок прыгал
    bool isJumping;//Прыгает ли персонаж

    bool affected = false;
    Vector3 affectedVector;
    float frame;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        oldDirection = transform.right.x;
        platformMask = LayerMask.GetMask("Platform");
    }

    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        ReadingJumpInput();
    }
    void FixedUpdate()
    {
        playerVelocity.x = horizontalAxis * horizontalVelocity;
        CheckBoundaries();
        RoofCheckHandling();
        GroundCheckHandling();
        Jumping();
        RotatePlayer();
        AnimatorChanges();
        if (affected)
        {
            playerVelocity += affectedVector;
            affected = false;
        }
        rigidbody.velocity = playerVelocity;

    }

    private void ReadingJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //ОБычный прыжок
            if (isGrounded)
            {
                audioSource.PlayOneShot(jumpSound);
                animator.SetTrigger("IsJumping");
                jumpFrames = 0;
                isJumping = true;
            }
        }
        //Если игрок отпустил кнопку прыжка то перестать прыгать
        if (!Input.GetButton("Jump"))
            isJumping = false;
    }

    private void Jumping()
    {
        if (isJumping)
        {
            playerVelocity.y = jumpAcceleration;
            jumpFrames++;
            if (jumpFrames == maxJumpFrames)
                isJumping = false;
        }
    }

    private void RoofCheckHandling()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, collider.size.y / 2 + .04f, platformMask))
            isJumping = false;
    }

    private void GroundCheckHandling()
    {
        if (isGrounded)
            playerVelocity.y = 0;
        else playerVelocity.y = Mathf.Clamp(playerVelocity.y-gravityAcceleration, -maxGravityVelocity, float.MaxValue);
    }

    private void RotatePlayer()
    {
        if (playerVelocity.x * oldDirection < 0)
        {
            transform.Rotate(0,180,0);
            oldDirection = playerVelocity.x;
        }
    }

    private void AnimatorChanges()
    {
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(playerVelocity.x));
    }

    private void CheckBoundaries()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, collider.size.y / 2 + .01f, platformMask);
    }

    public void AddVelocity(Vector3 velocity)
    {
       affected = true;
       affectedVector = velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        playerVelocity = velocity;
        rigidbody.velocity = velocity;
    }
}