using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    private CapsuleCollider2D hitBox;
    private SpriteRenderer rend;
    public Sprite[] sprites;
    public Transform player;
    public float knockback;


    public float chaseTimer;
    void Awake()
    {
        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        hitBox = GetComponent<CapsuleCollider2D>();
    
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
        transform.LookAt(player, Vector2.up);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //damage
        }
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
