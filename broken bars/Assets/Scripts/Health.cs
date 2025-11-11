using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider;
    public float health = 100;
    public Animator animator;
    private bool Candie = true;
    public BoxCollider boxCollider;
    public GameObject Impacteffect;
    public EnemyScript Script;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        Script = GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0 && Candie)
        {
            die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            BulletStats bullet = collision.gameObject.GetComponent<BulletStats>();
            takeDamage(bullet.damage);
            Instantiate(Impacteffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));

        }
    }

    public void die()
    {
        Script.canshoot = false;
        boxCollider.enabled = false;
        Candie = false;
        animator.SetTrigger("Die");
    }
}
