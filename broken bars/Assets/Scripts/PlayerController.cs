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

    [Header("Animations")]
    public Animator animator;

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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }

        yvelo = rb.velocity.y;
        xvelo = rb.velocity.x;

        if(!IsGrounded && yvelo <= 0)
        {
            IsFalling = true;
        }
        else
        {
            IsFalling = false;
        }

        
    }

    private void handleAnims()
    {
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
            Vector3 y = rb.velocity;
            y.y = rb.velocity.y * fallMult;
            rb.velocity = y;

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
