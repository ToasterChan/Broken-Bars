using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 20f; // Speed of the dash
    public float dashDuration = 0.2f;  // How long the dash lasts
    public float dashCooldown = 1f; //Time befrore the player can dash again

    public Rigidbody rb;
    public bool isDashing = false;
    private float dashTime;
    public float lastDashTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for dash input and cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            StartDash();
            Debug.Log("input oressed");
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            PDash();

        }
    }

    void StartDash()
    {
        Debug.Log("Dash started");
        isDashing = true;
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;
    }

    void PDash()
    {
        
        // Apply dash movement in the direction of input
        if (Time.time < dashTime)
        {
            Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            rb.velocity = dashDirection * dashSpeed;

            // End dash after duration
            if (Time.time >= dashTime)
            {
                isDashing = false;
                rb.velocity = Vector2.zero; // Stop movement after dash

            }
        }
    }
}


    