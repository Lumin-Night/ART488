using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private Rigidbody2D _rigidbody;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask Ground;
    private float airTimeCounter;
    public float floatTime;
    private bool isJumping;
    private float moveInput;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //Movement Script
        moveInput = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(moveInput * MovementSpeed, _rigidbody.velocity.y);

        //Facing Script
        if (!Mathf.Approximately(0, moveInput))
        {
            transform.rotation = moveInput > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
    }
    private void Update()
    {
        //Jump Script
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);

        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            airTimeCounter = floatTime;
            _rigidbody.velocity = Vector2.up * JumpForce;
        }
        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (airTimeCounter > 0)
            {
                _rigidbody.velocity = Vector2.up * JumpForce;
                airTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
}