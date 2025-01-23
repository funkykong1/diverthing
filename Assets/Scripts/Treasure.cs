using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Treasure : MonoBehaviour
{

    public string treasureName;
    public GameObject pickup;
    public Transform pickupspot;


    // Update is called once per frame
    void Update()
    {
        //glimmer anim here
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !GetComponent<Crawler>())
        {
            PickMeUp();
        }
    } 

    public void PickMeUp()
    {
        Debug.Log("Obtained " + treasureName + "!");
        UIManager.UpdateScore(1);
        //TreasureMaster.allItems.Remove(gameObject);
        Instantiate(pickup, pickupspot.position, Quaternion.identity);
        Destroy(gameObject);
    }


    
}