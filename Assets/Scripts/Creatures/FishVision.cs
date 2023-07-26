using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishVision : MonoBehaviour
{
    private CircleCollider2D vision;
    private Fish fish;
    void Start()
    {
        fish = GetComponentInParent<Fish>();
        vision = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            fish.ChaseRefresh();
        }
    }
}