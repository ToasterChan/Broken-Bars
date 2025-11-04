using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && canshoot && !reloading)
        {
            Shoot();
            
            timer = 0f;
        }

        if(timer < cooldown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            canshoot = true;
        }
    }
    public void Shoot()
    {
        if (ammo > 0)
        {
            canshoot = false;
            Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
            ammo--;
        }

        if(ammo == 0)
        {
            canshoot = false;
            StartCoroutine(reload());
        }
    }

    public IEnumerator reload()
    {
        reloading = true;
        canshoot = false;
        yield return new WaitForSeconds(Reloadtime);
        ammo = maxammo;
        canshoot = true;
        reloading = false;
    }
}
