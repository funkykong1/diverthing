using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Fish : MonoBehaviour
{
    private AIPath aiPath;
    private CircleCollider2D circle;
    private BoxCollider2D box;
    private SpriteRenderer rend;
    public Sprite[] sprites;

    public float chaseTimer;
    public bool chasing;

    void Awake()
    {
        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        aiPath = GetComponent<AIPath>();
    
        chaseTimer = 0;
    }

    void Start()
    {

    }

    void Update()
    {
        //flip the sprite
        if(aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if(aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

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
            rend.sprite = sprites[0];
            chasing = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            ChaseRefresh();
            rend.sprite = sprites[1];
    }

    void ChaseRefresh()
    {
        //refresh coroutine
        if(chasing)
            StopCoroutine(StopChasing());

        //will go at the player
        chasing = true;
        //if player re-enters the field of vision, resets timer
        chaseTimer = Random.Range(6f, 10f);

        StartCoroutine(StopChasing());
    }
}
