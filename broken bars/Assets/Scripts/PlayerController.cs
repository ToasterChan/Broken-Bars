using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Declare")]
    public Rigidbody rb;
    public Transform player;

    [Header("Movement settings")]
    public float moveSpeed;
    private float HorizontalMove;

    [Header("Ground check")]
    public float dist;
    public LayerMask mask;

    [Header("Jump settings")]
    public float jumpheight;
    public bool IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        IsGrounded = Physics.Raycast(player.position, Vector3.down, dist, mask);
       

        HorizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 V = rb.velocity;
        V.x = HorizontalMove * moveSpeed;
        rb.velocity = V;

        
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpheight * 100);
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = player.position;
        Vector3 end = origin + Vector3.down * dist;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, end);

    }

}
