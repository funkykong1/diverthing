using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    private CapsuleCollider2D maw;
    private Worm worm;
    void Start()
    {
        worm = GetComponentInParent<Worm>();
        maw = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
        
        Vector2 dir = new Vector2(other.transform.position.x - this.transform.position.x,other.transform.position.y - this.transform.position.y );
        other.GetComponent<Rigidbody2D>().AddForce(dir * worm.knockback, ForceMode2D.Impulse);

        //deal damage too herer
        
        

        }
    }
}
