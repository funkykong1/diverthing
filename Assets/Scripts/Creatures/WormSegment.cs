using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegment : MonoBehaviour
{
    private BoxCollider2D box;
    private Worm worm;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        worm = GetComponentInParent<Worm>();
        rb = GetComponent<Rigidbody2D>(); 


        //ignore collisions with ground and other creatures (and the worm itself)
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            worm.ChaseRefresh();


        }
    }
}
