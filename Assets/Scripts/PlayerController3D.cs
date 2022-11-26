using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class PlayerController3D : MonoBehaviour
{
    //Standard Movement
    private PlayerInput MabelInput;
    private InputAction moveAction;
    private InputAction Dash;
    private InputAction Jump;
    private CharacterController controller;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    public float rotationSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private bool isDashing;
    [SerializeField] private Vector3 moveDirection;
    public Vector2 moveXZ;
    private Vector3 velocity;
    public Animator characterAnimatior;
    [SerializeField] private LayerMask MovePlatform;
    public GameObject MabelModel;
        
    //Boundary Detection
    [SerializeField] private bool isSlow;
    [SerializeField] private float multiplier;

    //Air Movement
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private int jumps = 2;
    [SerializeField] private int dashCount = 1;
    [SerializeField] private float dashSpeed;
    [SerializeField] private bool isAirDashing;
    [SerializeField] private float airDashTime;
    [SerializeField] private float deceleration = 25;
    [SerializeField] private float jump;
    [SerializeField] private bool isJumping;
    [SerializeField] private int wallJumper;
    [SerializeField] private int wallJumped;

    //WallJump
    [SerializeField] private string WallJumpLayerName = "WallJump";
    private int wallJump;
    [SerializeField] private bool isWallJumping;


    //Falling Off
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private string KillZoneLayerName = "KillZone";
    [SerializeField] private string IntroLayerName = "Intro";
    private int killZone;
    [SerializeField] private float respawnTime;
    [SerializeField] private bool respawning;

    //UI
    public int Collected;
    public TMP_Text CollectibleText;
    public Animator uiAnimations;
    private bool intro;
    private int Intro;


    private void Awake()
    {
        MabelInput = GetComponent<PlayerInput>();
        Jump = MabelInput.actions["Jump"];
        moveAction = MabelInput.actions["Move"];
        Dash = MabelInput.actions["Dash"];
        Dash.performed += DashAction;
        Dash.canceled += DashEnd;
        Jump.performed += JumpAction;
    }


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        killZone = LayerMask.NameToLayer(KillZoneLayerName);
        wallJump = LayerMask.NameToLayer(WallJumpLayerName);
        Intro = LayerMask.NameToLayer(IntroLayerName);
    }
    private void Update()
    {
        Move();
        uiAnimations.SetInteger("Jumps", jumps);
        uiAnimations.SetInteger("DashSpent", dashCount);
        MovementAnimation();
    }
    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumps = 2;
            gravity = -20;
            currentPosition = this.transform.position;
            lastPosition = currentPosition - (moveDirection / 4);
            jumpHeight = 1.5f;
            isJumping = false;
            wallJumper = 0;
            wallJumped = 1;

        }
        moveXZ = moveAction.ReadValue<Vector2>();
 
        if (isSlow == true)
        {
            if(multiplier >= .3)
            {
                multiplier -= .03f;
            }
        }

        if (moveXZ != Vector2.zero && !isDashing)
        {
            moveSpeed = walkSpeed;
        }
        else if (moveXZ != Vector2.zero && isDashing)
        {
            moveSpeed = runSpeed;
        }
        else if (moveXZ == Vector2.zero)
        {
            moveSpeed = 0;
        }

        moveSpeed = moveSpeed * multiplier;

        moveDirection = new Vector3(moveXZ.x * moveSpeed, 0, moveXZ.y * moveSpeed);

        if (moveDirection != Vector3.zero)
        {
            //MabelModel.transform.forward = moveDirection;
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);

            MabelModel.transform.rotation = Quaternion.RotateTowards(MabelModel.transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }


        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;  
        controller.Move(velocity * Time.deltaTime);
        
        AirDash();

        if (isWallJumping && !isJumping)
        {
            gravity = -5;            
        }
        else if(isJumping)
        {
            gravity = -20;
        }
    }
    private void JumpAction(InputAction.CallbackContext context)
    {
        if (jumps > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumps--;
            dashCount = 1;
            isJumping = true;
        }
    }
    private void DashAction(InputAction.CallbackContext context)
    {
        isDashing = true;
    }
    private void DashEnd(InputAction.CallbackContext context)
    {
        isDashing = false;
    }

    public void SetSlow()
    {
        isSlow = true;
    }
    public void ClearSlow()
    {
        isSlow = false;
        multiplier = 1;
    }
    public void AirDash()
    {
        if (isDashing && isGrounded == false && dashCount > 0)
        {
            dashSpeed = 5;
            isAirDashing = true;
            dashCount--;
      
        }
        else if (isAirDashing)
        {
            dashSpeed -= deceleration * Time.deltaTime;
            controller.Move(moveDirection * (dashSpeed*Time.deltaTime));
            if (dashSpeed <= 0) isAirDashing = false;
        }
        
    }
    
    //Falloff Respawn and Wall Jump
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == killZone)
        {
            controller.enabled = false;
            transform.position = lastPosition;
            controller.enabled = true;
        }

        if (other.gameObject.layer == wallJump)
        {
            wallJumper = other.GetInstanceID();
                isJumping = false;
                isWallJumping = true;
                velocity.y = 0;
                dashCount = 1;
                jumpHeight *= 5;
                if (wallJumper != wallJumped)
                {
                    jumps++;
                }
            else
            {
                jumpHeight *= .12f;
                jumps++;
            }
        }
        if (other.gameObject.layer == Intro)
        {
            intro = true;
        }
        else intro = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == wallJump)
        {
            gravity = -20;
            jumpHeight = 1.5f;
            isWallJumping = false;
            wallJumped = wallJumper;
        }
    }
    public void MovementAnimation()
    {
        characterAnimatior.SetFloat("Speed", moveSpeed);
        characterAnimatior.SetBool("isWallJumping", isWallJumping);
        characterAnimatior.SetBool("isJumping", isJumping);
        characterAnimatior.SetBool("isDashing", isAirDashing);
        characterAnimatior.SetBool("Intro", intro);
    }
}