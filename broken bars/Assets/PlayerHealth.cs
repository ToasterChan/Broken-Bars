using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public int maxhealth = 5;
    public Image image;   // Start is called before the first frame update
    void Start()
    {
        image.fillAmount = health / 10;   
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = health / 10;

        if(health > maxhealth)
        {
            health = maxhealth;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
           Debug.Log("hit");
           BulletStats bullet = collision.gameObject.GetComponent<BulletStats>();
           takedamage(bullet.damage);
        }
    }

    public void takedamage(float damage)
    {
        health -= damage;
    }
}
