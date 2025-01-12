﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public int BulletSpeed;
    private Rigidbody2D rb2D;
    [HideInInspector] public Vector2 direction;
    public int Damage ;
    public Vector3 forward;

    Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        rb2D.AddForce(direction * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag("Enemie"))
        {
            if (other.gameObject.name == "FlyingEnemie")
            {
                print("HIT FLYING ALEIN");
                other.gameObject.GetComponentInChildren<FlyingEnemie>().HP = other.gameObject.GetComponentInChildren<FlyingEnemie>().HP - Damage;
                m_animator = other.gameObject.transform.GetComponentInChildren<Animator>();
                m_animator.SetTrigger("Shoot");
            }
            else if(other.gameObject.name == "meleEnemie")
            {

                other.gameObject.transform.GetComponentInChildren<MeleEnemie>().HP = other.gameObject.GetComponentInChildren<MeleEnemie>().HP - Damage;
                m_animator = other.gameObject.transform.GetComponentInChildren<Animator>();
                m_animator.SetTrigger("Shoot");

            }
            else if (other.gameObject.name == "shootingAlien")
            {
                other.gameObject.GetComponent<StandardEnemie>().HP = other.gameObject.GetComponent<StandardEnemie>().HP - Damage;

            }

            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<BossController>().HP -= Damage;

            if (other.gameObject.GetComponent<KamikazePlayer>().enabled)
            {
                other.gameObject.GetComponent<KamikazePlayer>().ChangeState();

            }
        }
        Destroy(this.gameObject);
      
    }
    
}
