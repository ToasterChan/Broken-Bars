using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSize : MonoBehaviour
{
    public Vector3 minsize;
    public Vector3 maxsize;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(
            Random.Range(minsize.x, maxsize.x),
            Random.Range(minsize.y, maxsize.y),
            Random.Range(minsize.z, maxsize.z)
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
