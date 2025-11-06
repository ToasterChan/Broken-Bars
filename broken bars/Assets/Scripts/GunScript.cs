using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform firepoint;
    public float cooldown;
    private float timer;
    public bool canshoot;
    public bool reloading;
    public int ammo;
    public int maxammo;
    public float Reloadtime;

    public Transform FlashLocation;
    public GameObject MuzzleEffect;
    public Color color;
    public SpriteRenderer spriteRenderer;
    public float fadeDuration = 0.5f;

    public PlayerController player;
    public TMP_Text text;

    private Coroutine fadeCoroutine;

    public float timesinceLastFire;

    [Header("Shake Settings")]
    public float shakeMagnitude;
    public float shakeTime;

    [Header("UI")]
    public Slider reloadSlider; // Reference to the reload slider
    public CanvasGroup reloadSliderCanvasGroup; // CanvasGroup for fading the slider

    private float reloadTimer = 0f;

    void Start()
    {
        spriteRenderer.color = new Color(0, 0, 0, 0);

        // Initialize slider visibility
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false);
        }
        if (reloadSliderCanvasGroup != null)
        {
            reloadSliderCanvasGroup.alpha = 0f;
        }
    }

    void Update()
    {

        text.text = ammo.ToString();
        if (Input.GetMouseButton(1) && canshoot && !reloading)
        {
            Shoot();
            timer = 0f;
        }

        timesinceLastFire += Time.deltaTime;

        if (timer < cooldown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            canshoot = true;
        }

        // Update reload slider if reloading
        if (reloading && reloadSlider != null)
        {
            reloadSlider.value = Mathf.Clamp01(reloadTimer / Reloadtime);
        }
    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            CineShake.bleg.ShakeCamera(shakeMagnitude, shakeTime);
            timesinceLastFire = 0f;
            canshoot = false;
            Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
            ammo--;

            StartFade(1f);
        }

        if (ammo == 0)
        {
            canshoot = false;
            StartCoroutine(reload());
        }
    }

    public IEnumerator reload()
    {
        reloading = true;
        canshoot = false;

        // Show and fade in the reload slider
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.maxValue = 1f;
            reloadSlider.value = 0f;
        }
        if (reloadSliderCanvasGroup != null)
        {
            StartCoroutine(FadeCanvasGroup(reloadSliderCanvasGroup, 1f, fadeDuration));
        }

        reloadTimer = 0f;
        while (reloadTimer < Reloadtime)
        {
            reloadTimer += Time.deltaTime;
            if (reloadSlider != null)
            {
                reloadSlider.value = Mathf.Clamp01(reloadTimer / Reloadtime);
            }
            yield return null;
        }

        ammo = maxammo;
        canshoot = true;
        reloading = false;

        // Fade out and hide the reload slider
        if (reloadSliderCanvasGroup != null)
        {
            StartCoroutine(FadeCanvasGroup(reloadSliderCanvasGroup, 0f, fadeDuration));
        }
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false);
        }
    }

    public void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeTo(targetAlpha));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = spriteRenderer.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha);

        if (targetAlpha == 1f)
        {
            yield return new WaitForSeconds(0.5f);
            StartFade(0f);
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float targetAlpha, float duration)
    {
        float startAlpha = cg.alpha;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }
        cg.alpha = targetAlpha;
    }
}