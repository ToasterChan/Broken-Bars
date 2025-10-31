using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Declare")]
    public Rigidbody rb;
    public Transform player;
    public Transform playerBody;

    [Header("Animations")]
    public Animator animator;
    public bool Facing; // off for left

    [Header("Movement settings")]
    public float moveSpeed;
    private float HorizontalMove;

    [Header("Ground check")]
    public float dist;
    public LayerMask mask;

    [Header("Jump settings")]
    public float jumpheight;
    public bool IsGrounded;
    public bool IsJumping;
    public bool IsFalling;
    public float fallMult;

    [Header("Jump Smoothing")]
    public float fallSmoothingTime = 0.2f; // Duration of smoothing in seconds
    private float fallSmoothingTimer = 0f;
    private bool wasJumpingLastFrame = false;

    [Header("Jump Timing")]
    public float coyoteTime = 0.2f; // Time after leaving ground to allow jumping
    public float jumpBufferTime = 0.2f; // Time to buffer jump input
    private float coyoteTimer = 0f;
    private float jumpBufferTimer = 0f;

    [Header("Debugging")]
    public float yvelo;
    public float xvelo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        handleAnims();

        IsGrounded = Physics.Raycast(player.position, Vector3.down, dist, mask);

        HorizontalMove = Input.GetAxis("Horizontal");

        // Update coyote timer
        if (IsGrounded)
        {
            coyoteTimer = coyoteTime; // Reset coyote timer when grounded
        }
        else
        {
            IsFalling = true;
            coyoteTimer -= Time.deltaTime; // Decrease timer when not grounded
        }

        // Update jump buffer timer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferTime; // Reset jump buffer timer when jump is pressed
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime; // Decrease timer otherwise
        }

        // Handle jump logic with coyote time and jump buffering
        if (jumpBufferTimer > 0 && coyoteTimer > 0)
        {
            Jump();
            jumpBufferTimer = 0; // Consume the buffered jump
        }

        if (Input.GetKeyUp(KeyCode.Space) && IsJumping)
        {
            Vector3 yvel = rb.velocity;
            yvel.y = 0;
            rb.velocity = yvel;
        }

        yvelo = rb.velocity.y;
        xvelo = rb.velocity.x;

        if (!IsGrounded && yvelo <= 0)
        {
            IsFalling = true;
        }
        else
        {
            IsFalling = false;
        }

        // Start smoothing when transitioning from jumping to falling
        if (IsFalling && wasJumpingLastFrame)
        {
            fallSmoothingTimer = fallSmoothingTime;
        }
        wasJumpingLastFrame = IsJumping;
    }

    private void handleAnims()
    {
        // rotate
        if (Input.GetKeyDown(KeyCode.D) && Facing == true)
        {
            playerBody.Rotate(new Vector3(0, 180, 0));
            Facing = false;
        }
        if (Input.GetKeyDown(KeyCode.A) && Facing == false)
        {
            playerBody.Rotate(new Vector3(0, 180, 0));
            Facing = true;
        }

        if (xvelo > 0 || xvelo < 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (IsJumping)
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }

        if (IsFalling)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }


    }

    private void FixedUpdate()
    {
        Move();
        fall();
    }

    private void Move()
    {
        Vector3 V = rb.velocity;
        V.x = HorizontalMove * moveSpeed;
        rb.velocity = V;

        
    }

    private void fall()
    {
        if (IsFalling)
        {
            IsJumping = false;

            // Gradually increase the downward force for a smoother transition
            rb.AddForce(Vector3.down * fallMult);
        }
        else if (!IsGrounded && !Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            // Apply jump release smoothing when the jump button is released
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.fixedDeltaTime;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpheight * 100);
        IsJumping = true;
       
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = player.position;
        Vector3 end = origin + Vector3.down * dist;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, end);

    }

}
