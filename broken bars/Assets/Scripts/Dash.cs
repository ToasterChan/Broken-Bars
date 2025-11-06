using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(100)]
public class Dash : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.Q;

    public GameObject DashEffect;
    public Transform dashlocationEffect;

    public Slider cooldownSlider; // Reference to the cooldown slider
    public CanvasGroup sliderCanvasGroup; // For fading out the slider

    private Rigidbody rb;
    private bool isDashing;
    private float dashTime;
    private float lastDashTime;
    private Vector3 dashDirection3D;

    public bool IsDashing => isDashing;

    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        // Initialize the slider
        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = dashCooldown;
            cooldownSlider.value = dashCooldown;
            sliderCanvasGroup.alpha = 0f; // Start hidden
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey) && Time.time >= lastDashTime + dashCooldown)
            StartDash();

        if (isDashing)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        // Update the slider value and visibility
        if (cooldownSlider != null)
        {
            float timeSinceLastDash = Time.time - lastDashTime;

            // Update the slider value to fill up during cooldown
            cooldownSlider.value = Mathf.Clamp(timeSinceLastDash, 0, dashCooldown);

            // Make the slider visible during cooldown
            if (timeSinceLastDash < dashCooldown)
            {
                sliderCanvasGroup.alpha = 1f; // Ensure it's visible
            }
            else if (timeSinceLastDash >= dashCooldown && sliderCanvasGroup.alpha > 0f)
            {
                StartCoroutine(FadeOutSlider());
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing) PDash();
    }

    void StartDash()
    {
        if (playerController != null)
        {
            dashDirection3D = playerController.Facing ? Vector3.left : Vector3.right;
        }
        else
        {
            dashDirection3D = Vector3.right;
        }

        isDashing = true;
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;

        // Make the slider visible immediately
        if (sliderCanvasGroup != null)
        {
            sliderCanvasGroup.alpha = 1f;
            cooldownSlider.value = 0f; // Start the cooldown from 0
        }
    }

    void PDash()
    {
        if (Time.time < dashTime)
        {
            Instantiate(DashEffect, dashlocationEffect.position, dashlocationEffect.rotation);

            float currentY = rb.velocity.y;
            rb.velocity = new Vector3(dashDirection3D.x * dashSpeed, currentY, 0f);
        }
        else
        {
            isDashing = false;
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    private IEnumerator FadeOutSlider()
    {
        float fadeDuration = 0.1f; // Duration of the fade-out
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            sliderCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        sliderCanvasGroup.alpha = 0f;
    }
}