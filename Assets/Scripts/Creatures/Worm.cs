using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    private CapsuleCollider2D hitBox;
    private SpriteRenderer rend;
    private CircleCollider2D circle;
    private Rigidbody2D rb;

    public Sprite[] sprites;
    public Transform player;
    public float knockback;


    public float chaseTimer;
    void Awake()
    {
        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        hitBox = GetComponent<CapsuleCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();   
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
    
        chaseTimer = 0;
    }

    void Start()
    {
        rend.sprite = sprites[0];
    }

    void Update()
    {

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
        transform.LookAt(player, Vector2.up);
    }
    public void Bite()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            Vector2 dir = new Vector2(other.transform.position.x - this.transform.position.x,other.transform.position.y - this.transform.position.y );
            other.rigidbody.AddForce(dir * knockback, ForceMode2D.Impulse);

            //deal damage too herer
        }
    }


    public void ChaseRefresh()
    {
        //if player re-enters the field of vision, resets timer
        chaseTimer = Random.Range(15f, 20f);
    }
}
