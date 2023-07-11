using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Shrimp : MonoBehaviour
{
    private AIPath aiPath;
    private AIDestinationSetter setter;

    private CircleCollider2D circle;
    private CapsuleCollider2D hitBox;
    private SpriteRenderer rend;
    public Sprite[] sprites;

    private GameObject targetPoint;



    public float RunTimer;
    void Awake()
    {

        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
        aiPath = GetComponent<AIPath>();
        setter = GetComponent<AIDestinationSetter>();

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
        {
            Run();
        }
        else
        {
            //eventually change this to an idle thing
            aiPath.enabled = false;
            
            rend.sprite = sprites[0];
        }

        if(aiPath.reachedEndOfPath)
            RunTimer = 0;

        if(targetPoint)
        {}
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
        
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
        GameObject targetPoint = points[Random.Range(0, points.Length)];

        float dist = Vector2.Distance(targetPoint.transform.position, transform.position);


        //only get new path once after plr comes close
        if(RunTimer <= 0)
        {
            setter.target = targetPoint.transform;
        }


        //if player re-enters the field of vision, resets timer
        RunTimer = Random.Range(6f, 10f);
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

