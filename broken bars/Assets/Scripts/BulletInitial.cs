using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInitial : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f;
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
        Destroy(gameObject);
    }
}

