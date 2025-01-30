using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{

    private UIManager uiManager;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && uiManager.score >= uiManager.goal)
        {
            //TODO gamemanager win
            Debug.Log("Voitit pelin :D");
            GameObject.Find("Win Canvas").SetActive(true);
        }
    }
}
