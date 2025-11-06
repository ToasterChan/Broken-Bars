using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class BulletInitial : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f;
    public GameObject Effect;

    [Header("Shake Settings")]
    public float shakeMagnitude;
    public float shakeTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.right * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(Effect, transform.position, Quaternion.identity);
        CineShake.bleg.ShakeCamera(shakeMagnitude, shakeTime);
        Destroy(gameObject);
        
    }
}

