using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private Crawler parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Crawler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            parent.scaredTimer = 200;
            StartCoroutine(parent.Hide());
        }   
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if(other.CompareTag("Ground"))
            parent.Flip();
    }


    //void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.CompareTag("Ground"))
    //         parent.RotateDown();
    // }

}
