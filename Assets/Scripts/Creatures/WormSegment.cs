using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegment : MonoBehaviour
{
    private Worm worm;

    // Start is called before the first frame update
    void Start()
    {
        worm = GetComponentInParent<Worm>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            worm.ChaseRefresh();
        }
    }
}
