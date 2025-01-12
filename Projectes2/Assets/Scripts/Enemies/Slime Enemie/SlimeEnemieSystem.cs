﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemieSystem : MonoBehaviour
{


 
    public int health = 10;

    public float chaseDistance = 10;
    public float attackDistance = 7;
    public float chaseSpeed = 4;

    private GameObject target;
    private float distance;

    public Transform[] spawnPoints;
    public GameObject[] enemie;

    public Rigidbody2D rb2d;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        rb2d = GetComponent<Rigidbody2D>();


        if (transform.childCount > 1)
        {
            spawnPoints[0] = this.gameObject.transform.GetChild(0);
            spawnPoints[1] = this.gameObject.transform.GetChild(1);
        }
        else
        {
            print("THis slime has no childern");
        }

    }

    void Update()
    {
        EnemieReproduction();
        StateMachineEnemie();

        
    }

    private void FixedUpdate()
    {
        StateMachineEnemie();
    }

    void EnemieReproduction()
    {
        if (enemie.Length == 0 && health <= 0)
        {
            Destroy(this.gameObject);
        }
        else if (health <= 0 && enemie.Length > 0)
        {
            Instantiate(enemie[0], spawnPoints[0].position, Quaternion.identity);
            Instantiate(enemie[0], spawnPoints[1].position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void StateMachineEnemie()
    {
        distance = (target.transform.position.x - rb2d.position.x);


        distance = Mathf.Abs(distance);

        //chase
        if ((distance <= chaseDistance && distance > attackDistance))
        {
            rb2d.position = Vector2.MoveTowards(rb2d.position, new Vector2(target.transform.position.x, 0), chaseSpeed * Time.deltaTime);
        }

        ////patrol
        //else if (distance > chaseDistance)
        //{
        //    print("Patrol");
        //}

        ////shoot
        //else if (distance <= attackDistance)
        //{
        //    print("Attack");
            
        //}

       
    }


}
