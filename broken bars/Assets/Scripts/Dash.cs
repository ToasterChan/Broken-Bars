using System;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class Dash : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.Q;

    private Rigidbody rb;
    private bool isDashing;
    private float dashTime;
    private float lastDashTime;
    private Vector3 dashDirection3D;

    public bool IsDashing => isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey) && Time.time >= lastDashTime + dashCooldown)
            StartDash();
    }

    void FixedUpdate()
    {
        if (isDashing) PDash();
    }

    void StartDash()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(inputX) > 0f)
            dashDirection3D = new Vector3(Mathf.Sign(inputX), 0f, 0f);
        else
        {
            float facingSign = 0f;
            if (Mathf.Abs(transform.right.x) > 0.001f) facingSign = Mathf.Sign(transform.right.x);
            else if (Mathf.Abs(transform.forward.x) > 0.001f) facingSign = Mathf.Sign(transform.forward.x);
            else facingSign = 1f;
            dashDirection3D = new Vector3(facingSign, 0f, 0f);
        }
        isDashing = true;
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;
    }

    void PDash()
    {
        if (Time.time < dashTime)
        {
            float currentY = rb.velocity.y;
            rb.velocity = new Vector3(dashDirection3D.x * dashSpeed, currentY, 0f);
        }
        else
        {
            isDashing = false;
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
}