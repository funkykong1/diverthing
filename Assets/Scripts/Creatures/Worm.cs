using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Worm : MonoBehaviour
{
    public AIPath aiPath;
    private Seeker seeker;

    private CapsuleCollider2D hitBox;
    private SpriteRenderer rend;
    private CircleCollider2D circle;
    private Rigidbody2D rb;

    public Sprite[] sprites;


    public float chaseTimer;
    void Awake()
    {
        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        hitBox = GetComponent<CapsuleCollider2D>();
        circle = this.GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>(); 

        //ignore collisions with ground and other creatures (and the worm itself)
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);

        aiPath = GetComponent<AIPath>();
        seeker = GetComponent<Seeker>();
    
        chaseTimer = 0;
        
    }

    void Start()
    {
        //docile face
        rend.sprite = sprites[0];
        //let the worm roam through blocks
        aiPath.constrainInsideGraph = false;
        seeker.graphMask = 9;

        
    }

    void Update()
    {

        //flip the sprite
        if(aiPath.desiredVelocity.x >= 0.01f)
            rend.flipX = false;
        else if(aiPath.desiredVelocity.x <= -0.01f)
            rend.flipX = true;



        if(chaseTimer > 0)
            Chase();
        else
        {
            
            rend.sprite = sprites[0];
        }
    }
    void LateUpdate()
    {
        if(chaseTimer > 0)
            chaseTimer -= 1 * Time.deltaTime;
    }

    void Chase()
    {
        rend.sprite = sprites[1];
        //aiPath.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {

        }
    }


    public void ChaseRefresh()
    {
        //if player re-enters the field of vision, resets timer
        rend.sprite = sprites[1];
        chaseTimer = Random.Range(15f, 20f);
    }
}
