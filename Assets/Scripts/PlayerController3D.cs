using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController3D : MonoBehaviour
{
    //Standard Movement
    private CharacterController controller;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float depthCompensation;
    [SerializeField] private Vector3 moveDirection;
    private float moveX;
    private float moveZ;
    private Vector3 velocity;
    private float jumpForce;

    //Boundary Detection
    private bool isSlow;
    [SerializeField] private float multiplier;

    //Air Movement
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumps = 2;
    [SerializeField] private float dashCount = 1;
    [SerializeField] private float dashSpeed;
    [SerializeField] private bool isAirDashing;
    [SerializeField] private float airDashTime;
    [SerializeField] private float deceleration = 25;
    [SerializeField] private bool isJumping;

    //WallJump
    [SerializeField] private string WallJumpLayerName = "WallJump";
    private int wallJump;
    [SerializeField] private bool isWallJumping;


    //Falling Off
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private string KillZoneLayerName = "KillZone";
    private int killZone;
    [SerializeField] private float respawnTime;
    [SerializeField] private bool respawning;

    //Collect
    public int Collected;
    public TMP_Text CollectibleText;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        killZone = LayerMask.NameToLayer(KillZoneLayerName);
        wallJump = LayerMask.NameToLayer(WallJumpLayerName);


    }
    private void Update()
    {
        Move();
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
        }

        Movement();        
        
        if(isSlow == true)
        {
            if(multiplier >= .1)
            {
                multiplier -= .03f;
            }
        }

        moveDirection = new Vector3(moveDirection.x * (moveSpeed * multiplier), 0, moveDirection.z * (moveSpeed * multiplier));

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;  
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumps > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumps--;
            dashCount = 1;
            isJumping = true;
        }

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
    private void Movement()
    {

        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        if (moveDirection != Vector3.zero && !Input.GetButton("Sprint"))
        {
            moveSpeed = walkSpeed;
        }
        else if (moveDirection != Vector3.zero && Input.GetButton("Sprint") && isGrounded)
        {
            moveSpeed = runSpeed;
        }
        else if (moveDirection == Vector3.zero)
        {
            moveSpeed = 0;
        }
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
        if (Input.GetButtonDown("Sprint") && isGrounded == false && dashCount > 0)
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

    

    //Falloff Respawn
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
            isJumping = false;
            isWallJumping = true;
            velocity.y = 0;
            if(jumps <= 1)
            {
                jumps++;
            }
            dashCount = 1;
            jumpHeight *= 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == wallJump)
        {
            gravity = -20;
            jumpHeight = 1.5f;
            isWallJumping = false;
        }

    }

    public void Collect()
    {
        Collected++;
        CollectibleText.text = Collected.ToString();
    }

}