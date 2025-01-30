using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

Crawler crawler;

void Start()
{
    crawler = GetComponentInParent<Crawler>();
}

void OnTriggerEnter2D(Collider2D other)
    {
        //if player, hit it for one(1) dmg
        if(other.CompareTag("Player"))
        {
            Health health;
            if(health = other.GetComponent<Health>())
            {
                health.GetHit(1,transform.gameObject);
            }
        }

        if(other.CompareTag("Harpoon") && crawler.hp >= 0)
        {
            if(other.GetComponent<Harpoon>().timer <= 0)
                return;

            StopAllCoroutines();
            crawler.StartCoroutine(crawler.Struck());
        }

    }
}
