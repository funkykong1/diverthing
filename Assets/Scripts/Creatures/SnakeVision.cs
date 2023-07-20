using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeVision : MonoBehaviour
{
    private CircleCollider2D vision;
    private Worm worm;
    void Start()
    {
        worm = GetComponentInParent<Worm>();
        vision = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            worm.ChaseRefresh();
        }
    }
}
