using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Animator animator;
    public bool canAttack;
    public float coolDown;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack == true)
        {
            Attack();
        }

        if (!canAttack)
        {
            if(timer < coolDown)
            {
                timer += 1 * Time.deltaTime;
            }

            if(timer > coolDown)
            {
                canAttack = true;
            }
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        timer = 0;
        canAttack = false;
    }
}
