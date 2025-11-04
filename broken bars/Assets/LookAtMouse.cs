using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [Header("References")]
    public Transform gunHolder; 

    [Header("Options")]
    public float zOffset = 0f; 
    public bool flipWhenBehind = true; 

    Vector3 initialScale;

    void Start()
    {
        if (gunHolder == null)
            gunHolder = transform;
        initialScale = gunHolder.localScale;
    }

    void Update()
    {
        if (gunHolder == null)
            return;

        
        var cam = Camera.main;
        if (cam == null) return;

        Vector3 mouseScreen = Input.mousePosition;
        float cameraToPlane = -cam.transform.position.z; // works when scene is on z=0 and camera at negative z
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, cameraToPlane));
        mouseWorld.z = gunHolder.position.z; // keep same Z

        Vector3 dir = (mouseWorld - gunHolder.position);
        dir.z = 0f;
        if (dir.sqrMagnitude <= Mathf.Epsilon) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gunHolder.rotation = Quaternion.Euler(0f, 0f, angle + zOffset);

        if (flipWhenBehind)
        {
           
            bool pointingRight = angle > -90f && angle < 90f;
            Vector3 s = initialScale;
            s.y = pointingRight ? Mathf.Abs(initialScale.y) : -Mathf.Abs(initialScale.y);
            gunHolder.localScale = s;
        }
    }
}

