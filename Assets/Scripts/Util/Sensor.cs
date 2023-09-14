using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private CircleCollider2D forward;
    private BoxCollider2D down;
    private Crawler parent;

    // Start is called before the first frame update
    void Start()
    {
        forward = this.GetComponent<CircleCollider2D>();
        down = this.GetComponent<BoxCollider2D>();
        parent = GetComponentInParent<Crawler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
            StartCoroutine(parent.RotateUp());
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.CompareTag("Ground"))
    //         parent.RotateDown();
    // }

}
