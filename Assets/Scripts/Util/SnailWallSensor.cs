using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THERE ISNT A BETTER WAY TO DO THIS
public class SnailWallSensor : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            GetComponentInParent<Crawler>().Flip();
        }
    }
}
