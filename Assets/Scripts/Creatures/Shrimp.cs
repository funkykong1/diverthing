using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    private bool idleCoroutineWaiting, isRunning;

    public float distance, detectRange;
    public float runTimer;
    void Awake()
    {

        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        aiPath = GetComponent<AIPath>();

        runTimer = 0;
        detectRange = 2;
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

        //update distance between shrimp and plr
        distance = Vector2.Distance(this.transform.position, GameObject.Find("Player").transform.position);
        

        if(distance <= detectRange)
            RunRefresh();
        

        if(!aiPath.hasPath && isRunning)
            PathDone();


        if (!idleCoroutineWaiting && !isRunning)
        {
            idleCoroutineWaiting = true;
            StartCoroutine(Idle());
        }
        
    }
    void LateUpdate()
    {
        if(runTimer > 0)
            runTimer -= 1 * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Player"))
        {
            PickMeUp();
        }
    } 

    void RunRefresh()
    {
        
        if(idleCoroutineWaiting)
            StopCoroutine(Idle());

        //this makes the shrimp go for a rando spot
        //Can sometimes be dumb and go like 2 foot. Its a shrimp though
        if(!isRunning)
            GetRandomSpot(range*3);

        //bool for only one path and for other stuff
        isRunning = true;

        //run sprite
        rend.sprite = sprites[1];

        //shrimp is alert and goes faster and sees further
        detectRange = 3;
        aiPath.maxSpeed = 4.5f;

    }

    //when a fleeing AiPath is completed
    void PathDone()
    {
        Debug.Log("PATH COMPLETE");
        //shrimp checks for player again if running with slightly greater range
        if(distance <= detectRange * 1.5 && isRunning)
            RunRefresh();
        else
        //calms down if player isnt near anymore
        isRunning = false;
    }

    public IEnumerator Idle()
    {
        //shrimp no longer running round
        rend.sprite = sprites[0];


        //Shrimp randomly chooses spots to idle around, moves slower
        idleTimer = Random.Range(2f, 6f);
        aiPath.maxSpeed = 3f;

        GetRandomSpot(range);

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

