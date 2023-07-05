using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Treasure : MonoBehaviour
{

private RuleTile tile;

    // Update is called once per frame
    void Update()
    {
        //glimmer anim here
    }
    void Start()
    {
        
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PickMeUp();
        }
    } 

    public void PickMeUp()
    {
        Debug.Log(tile.name);
    }
}