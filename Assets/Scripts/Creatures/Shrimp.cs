using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class Shrimp : MonoBehaviour
{

    //shrimp mover


    //a*
    private AIPath aiPath;

    private CircleCollider2D circle;
    private SpriteRenderer rend;
    public Sprite[] sprites;

    [SerializeField]
    private float range, idleTimer;

    //Solely so update doesnt start 9000 coroutines
    [SerializeField]
    private bool idleCoroutineWaiting, isRunning;

    void Awake()
    {

        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        aiPath = GetComponent<AIPath>();
    }

    void Start()
    {
        rend.sprite = sprites[0];
    }

    void Update()
    {
        //flip the sprite
        if(aiPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if(aiPath.desiredVelocity.x <= -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        

        if(aiPath.reachedDestination && isRunning)
            StartCoroutine(PathDone());

        if (!idleCoroutineWaiting && !isRunning)
        {
            idleCoroutineWaiting = true;
            StartCoroutine(Idle());
        }
        
    }

    //if shrimp notices another creature it will run
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            RunRefresh();
        }
        //harpoons also scare off the shrimp
        else if(other.gameObject.CompareTag("Harpoon") && !isRunning)
            RunRefresh();
    }


    void RunRefresh()
    {
        
        StopCoroutine(Idle());
        idleCoroutineWaiting = false;
        //this makes the shrimp go for a rando spot
        //Can sometimes be dumb and go like 2 foot. Its a shrimp though
        GetRandomSpot(range*3);

        //bool for only one path and for other stuff
        isRunning = true;

        //run sprite
        rend.sprite = sprites[1];

        //shrimp is alert and goes faster and sees further
        circle.radius = 3;
        aiPath.maxSpeed = 5.5f;

    }

    //when a fleeing AiPath is completed
    IEnumerator PathDone()
    {
        //calms down if player isnt near anymore
        yield return new WaitForSeconds(4);
        if(aiPath.reachedDestination)
            isRunning = false;
        else
            yield break;
    }

    public IEnumerator Idle()
    {
        //shrimp no longer running round
        rend.sprite = sprites[0];


        //Shrimp randomly chooses spots to idle around, moves slower
        idleTimer = Random.Range(2f, 6f);
        aiPath.maxSpeed = 1.75f;
        circle.radius = 2;

        GetRandomSpot(range);

        yield return new WaitUntil(() => aiPath.reachedDestination);
        yield return new WaitForSeconds(idleTimer);

        //Shrimp range is smaller during its downtime. Something like its eating moss idk
        idleCoroutineWaiting = false;
    }

    //function for the random node thing
    private void GetRandomSpot(float radius)
    {
        //Only lets the critter use the "Grid Graph" for node searching (doesnt run into walls)
		GraphMask mask1 = GraphMask.FromGraphName("Grid Graph");

        //Nearest node constraint constructor
		NNConstraint nn = NNConstraint.Default;

        //set the constraint to the Grid Graph
		nn.graphMask = mask1;

        //get a random vector 2 in float radius
        Vector2 spot = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * radius;

        //Gets nearest walkable node to the vector3 given
        GraphNode node = AstarPath.active.GetNearest(spot, nn).node;

        //manually set destination without use of dest setter or seeker
        aiPath.destination = (Vector3)node.position;
    }

}

