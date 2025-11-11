using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public Transform gunPoint;
    public Transform firepoint;
    public GameObject bullet;
    public bool canshoot = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(burstfire());
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gunPoint.transform.LookAt(player.transform.position);
    }

    public IEnumerator burstfire()
    {
        if (canshoot)
        {
            Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
            yield return new WaitForSeconds(0.3f);
            Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
            yield return new WaitForSeconds(0.3f);
            Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
            yield return new WaitForSeconds(Random.Range(2, 4));
            StartCoroutine(burstfire());
        }
        

    }
}
