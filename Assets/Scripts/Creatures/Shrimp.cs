using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Shrimp : MonoBehaviour
{
    private AIPath aiPath;
    private CircleCollider2D circle;
    private CapsuleCollider2D hitBox;
    private SpriteRenderer rend;
    public Sprite[] sprites;


    public float RunTimer;
    void Awake()
    {
        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
        aiPath = GetComponent<AIPath>();

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Point").Length; i++)
        {
            
        }
    
        RunTimer = 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        //flip the sprite
        if(aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if(aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if(RunTimer > 0)
            Run();
        else
        {
            //eventually change this to an idle thing
            aiPath.enabled = false;
            
            rend.sprite = sprites[0];
        }
    }
    void LateUpdate()
    {
        if(RunTimer > 0)
            RunTimer -= 1 * Time.deltaTime;
    }

    void Run()
    {
        aiPath.enabled = true;
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name.Equals("Player"))
        {
            PickMeUp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            RunRefresh();
            rend.sprite = sprites[1];
        }
    }   

    void RunRefresh()
    {
        //if player re-enters the field of vision, resets timer
        RunTimer = Random.Range(10f, 18f);
    }

    void PickMeUp()
    {
        Debug.Log("Obtained a deep sea critter!");
        //UIManager.UpdateScore(1);
        //TreasureMaster.allItems.Remove(gameObject);
        //Instantiate(explosion, transform.position, Quaternion.identity);

        //if(TreasureMaster.allItems.Count == 0)
            //GameManager.StartEscape();


        Destroy(gameObject);
    }
}
