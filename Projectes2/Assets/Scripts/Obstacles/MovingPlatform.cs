﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    private bool notFreezed = true;
    public bool moveable = true;

    public ParticleSystem part1;
    public ParticleSystem part2;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;

        if (part1 != null && part2 != null)
        {
            part1.Stop();
            part2.Stop();
        }
    }

    private void Update()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(i).transform.CompareTag("TimeStop"))
            {
                transform.GetChild(0).GetChild(i).GetComponent<TimeStopPlatform>().reactivateTime.AddListener(Defreeze);
                notFreezed = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            moveable = true;
            if(part1 != null && part2 != null)
            {
                part1.Play();
                part2.Play();
            }

        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        else if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }


        if(notFreezed && moveable)
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
       Gizmos.DrawLine(pos1.position, pos2.position);
    }
    public void Defreeze()
    {
        notFreezed = true;
    }
}
