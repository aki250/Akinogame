using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    public float attackRate;
    public float LastAttackTime;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0)&&Time.time-LastAttackTime>attackRate) { 
            LastAttackTime = Time.time;
            anim.SetTrigger("Attack");
        }
    }
}
