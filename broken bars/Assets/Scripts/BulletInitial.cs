using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using System.Linq.Expressions;

public class BulletInitial : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 20f;
    public GameObject Effect;

    public bool grenade = false;

    [Header("Shake Settings")]
    public float shakeMagnitude;
    public float shakeTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.right * speed, ForceMode.Impulse);

        if (grenade)
        {
            StartCoroutine(blowup());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && grenade == true)
        {
            Instantiate(Effect, transform.position, Quaternion.identity);
            CineShake.bleg.ShakeCamera(shakeMagnitude, shakeTime);
            Destroy(gameObject);
        }


        if (!grenade)
        {
            Instantiate(Effect, transform.position, Quaternion.identity);
            CineShake.bleg.ShakeCamera(shakeMagnitude, shakeTime);
            Destroy(gameObject);
        }
       
        
    }

  

    IEnumerator blowup()
    {
        yield return new WaitForSeconds(4);
        Instantiate(Effect, transform.position, Quaternion.identity);
        CineShake.bleg.ShakeCamera(shakeMagnitude, shakeTime);
        Destroy(gameObject);
    }
}

