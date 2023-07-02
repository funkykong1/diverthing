using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private CircleCollider2D circle;
    private BoxCollider2D box;

    public float chaseTimer;
    public bool chasing;

    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();

        chaseTimer = 0;
    }

    void Update()
    {





        
        if(chasing)
            Chase();
    }

    void Chase()
    {
        
    }

    IEnumerator StopChasing()
    {
        while(chasing)
        {
            yield return new WaitForSeconds(chaseTimer);
            chasing = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            ChaseRefresh();
    }

    void ChaseRefresh()
    {
        //refresh coroutine
        if(chasing)
            StopCoroutine(StopChasing());

        //will go at the player
        chasing = true;
        //if player re-enters the field of vision, resets timer
        chaseTimer = Random.Range(6, 10);

        StartCoroutine(StopChasing());
    }
}
