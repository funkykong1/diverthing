using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegment : MonoBehaviour
{
    private BoxCollider2D box;
    private Worm worm;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        worm = GetComponentInParent<Worm>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            worm.ChaseRefresh();
        }
    } 
}
