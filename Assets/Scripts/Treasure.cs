using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Treasure : MonoBehaviour
{

    public string treasureName;

    // Update is called once per frame
    void Update()
    {
        //glimmer anim here
    }
    void Start()
    {

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PickMeUp();
        }
    } 

    public void PickMeUp()
    {
        Debug.Log("Obtained " + treasureName + "!");
        //UIManager.UpdateScore(scoreValue);
        //AlienMaster.allAliens.Remove(gameObject);
        //Instantiate(explosion, transform.position, Quaternion.identity);

        //if(AlienMaster.allAliens.Count == 0)
            //GameManager.SpawnNewWave();
        Destroy(gameObject);
    }
}