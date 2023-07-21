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
            Health health;
            if(health = other.GetComponent<Health>())
            {
                health.GetHit(1,transform.parent.gameObject);
            }
        }
    }
}
