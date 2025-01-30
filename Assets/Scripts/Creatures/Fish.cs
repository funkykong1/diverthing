using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Fish : MonoBehaviour
{

    //FISH AI

    //a* references
    private AIPath aiPath;
    private AIDestinationSetter setter;


    private CircleCollider2D circle;
    private CapsuleCollider2D hitBox;
    private SpriteRenderer rend;
    public Sprite[] sprites;

    [SerializeField]
    private float range, idleTimer;

    //Solely so update doesnt start 9000 coroutines
    private bool idleCoroutineWaiting = false;

    public float distance, detectRange;
    public float chaseTimer;
    void Awake()
    {
        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        hitBox = GetComponent<CapsuleCollider2D>();

        aiPath = GetComponent<AIPath>();
        setter = GetComponent<AIDestinationSetter>();


        chaseTimer = 0;
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

        //update distance between fish and plr
        distance = Vector2.Distance(this.transform.position, GameObject.Find("Player").transform.position);
        
        //IF CLOSE TO Player AND NOT CHASING, START CHASING
        //If already chasing, make range smaller
        //Makes it feel like the fish only keeps chasing if it thinks its getting u
        if(distance <= detectRange)
            ChaseRefresh();

        //IF chaseTimer not over, give chase
        if(chaseTimer > 0)
            Chase();
        //otherwise, begin idling process
        else if (!idleCoroutineWaiting)
        {

            //FISH CALM HERE
            idleCoroutineWaiting = true;
            StartCoroutine(Idle());
        }

    }
    void LateUpdate()
    {
        //Tick chasetimer down 
        if(chaseTimer > 0)
            chaseTimer -= 1 * Time.deltaTime;
    }

    void Chase()
    {
        //!!!food spotted!!! stop idle coroutine
        if(idleCoroutineWaiting)
            StopCoroutine(Idle());

        //manually change target and speed
        setter.target = GameObject.Find("Player").transform;
        aiPath.maxSpeed = 4.5f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player, hit it for one(1) dmg
        if(other.CompareTag("Player"))
        {
            Health health;
            if(health = other.GetComponent<Health>())
            {
                health.GetHit(1,transform.gameObject);
            }
        }
    }

    
    
    public void ChaseRefresh()
    {
        //Timer doesnt get refreshed but the detect range is smaller
        detectRange = 1;
        aiPath.maxSpeed = 3;
        rend.sprite = sprites[1];
        chaseTimer = Random.Range(10f, 18f);
    }
    public IEnumerator Idle()
    {
        //fish no longer angry
        rend.sprite = sprites[0];
        setter.target = null;

        


        //Fish randomly chooses spots to idle around, moves slower
        idleTimer = Random.Range(5f, 25f);
        aiPath.maxSpeed = 1.5f;

        //Only lets the fish use the "Grid Graph" for node searching (doesnt run into walls)
		GraphMask mask1 = GraphMask.FromGraphName("Grid Graph");

        //Nearest node constraint constructor
		NNConstraint nn = NNConstraint.Default;

        //set the constraint to the Grid Graph
		nn.graphMask = mask1;

        //get a random vector 2 in float range
        //'this' keywords probably moot
        Vector2 spot = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * range;

        //Gets nearest walkable node to the vector3 given
        GraphNode node = AstarPath.active.GetNearest(spot, nn).node;
        
        //manually set destination without use of dest setter or seeker
        aiPath.destination = (Vector3)node.position;


        yield return new WaitForSeconds(idleTimer);

        //Make the fish range larger here again, kind of simulating it having rested
        detectRange = 2;
        idleCoroutineWaiting = false;
    }
}
