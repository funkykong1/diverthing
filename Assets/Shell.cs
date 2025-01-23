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
        //if collider has health component (basically if its the player)
        if(other.GetComponent<Health>())
        {
            other.GetComponent<Health>().GetHit(1,transform.gameObject);
        }

        if(other.CompareTag("Harpoon") && crawler.hp >= 0)
        {
            if(other.GetComponent<Harpoon>().grounded)
                return;

            StopAllCoroutines();
            crawler.StartCoroutine(crawler.Struck());
        }

    }
}
