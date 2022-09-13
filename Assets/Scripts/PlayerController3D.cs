using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float depthCompensation;

    private Vector3 moveDirection;
    private float moveX;
    private float moveZ;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isSlow;
    [SerializeField] private float slowMultiplier = 0.65f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();

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
        }

        Movement();
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        float multiplier = (isSlow) ? slowMultiplier : 1;
        //moveDirection *= (moveSpeed * multiplier);
        moveDirection = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.z * (moveSpeed * multiplier));

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Movement()
    {
        moveZ = Input.GetAxis("Vertical") * depthCompensation;
        moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
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
    }

}
