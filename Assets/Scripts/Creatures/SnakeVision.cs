using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeVision : MonoBehaviour
{
    private CapsuleCollider2D vision;
    private Worm worm;
    void Start()
    {
        worm = GetComponentInChildren<Worm>();
        vision = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            worm.ChaseRefresh();
        }
    }
}
