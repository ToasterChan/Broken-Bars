using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomZ = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
