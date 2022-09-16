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
    [SerializeField] private float deceleration = 5;
 
    //Falling Off
    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private string KillZoneLayerName = "KillZone";
    private int killZone;

    //Collect
    public int Collected;
    public TMP_Text CollectibleText;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        killZone = LayerMask.NameToLayer(KillZoneLayerName);
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
            currentPosition = this.transform.position;
            lastPosition = currentPosition - (moveDirection / 4);
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
            
        }

        AirDash();
    }
    private void Movement()
    {

        moveZ = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && isGrounded)
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == false && dashCount > 0)
        {
            dashSpeed = 2;
            isAirDashing = true;
            dashCount--;

        }
        else if (isAirDashing)
        {
            Debug.Log("dash");
            dashSpeed -= deceleration * Time.deltaTime;
            controller.Move(moveDirection * dashSpeed);
            if (dashSpeed <= 0) isAirDashing = false;
        }
        
    }

    //Falloff Respawn
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == killZone)
        {
            controller.enabled = false;
            transform.position = lastPosition;
            controller.enabled = true;
           
        }
    }

    public void Collect()
    {
        Collected++;
        CollectibleText.text = Collected.ToString();
    }

}