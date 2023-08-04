using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Worm : MonoBehaviour
{
    //aiPath allows for node getting
    private AIPath aiPath;
    //Seeker allows for graph tinkering
    private Seeker seeker;
    //setter is for chasing player
    private AIDestinationSetter setter;
    //change sprite
    private SpriteRenderer rend;
    //snake head hitbox
    private CircleCollider2D circle;
    //angy and normal face
    public Sprite[] sprites;

    //privated so nothing else uses these ever
    [SerializeField]
    private float range, idleTimer;
    //When to get a new spot during idle coroutine
    private bool idleCoroutineWaiting;

    //distance to player, player detection range
    public float distance, detectRange;

    //how long chase player
    public float chaseTimer;

    
    void Awake()
    {
        //initialize stuff
        rend = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();


        //ignore collisions with ground and other creatures (and the worm itself)
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);

        //ai path
        aiPath = GetComponent<AIPath>();
        seeker = GetComponent<Seeker>();
        setter = GetComponent<AIDestinationSetter>();
    
        chaseTimer = 0;
        
    }

    void Start()
    {
        //docile face
        rend.sprite = sprites[0];
        //worm moves completely freely regardless of graph
        aiPath.constrainInsideGraph = false;
        //let the worm roam through blocks
        seeker.graphMask = 9;

        
    }

    void Update()
    {

        //flip the sprite
        if(aiPath.desiredVelocity.x >= 0.01f)
            rend.flipX = false;
        else if(aiPath.desiredVelocity.x <= -0.01f)
            rend.flipX = true;

        //update distance between [worm] and plr
        distance = Vector2.Distance(this.transform.position, GameObject.Find("Player").transform.position);
        

        if(distance <= detectRange)
            ChaseRefresh();

        //IF chaseTimer not over, give chase
        if(chaseTimer > 0)
            Chase();
        //otherwise, begin idling process
        else if (!idleCoroutineWaiting)
        {

            //worm CALM HERE
            idleCoroutineWaiting = true;
            StartCoroutine(Idle());
        }


    }
    void LateUpdate()
    {
        if(chaseTimer > 0)
            chaseTimer -= 1 * Time.deltaTime;
    }

    void Chase()
    {
        //!!!food spotted!!! stop idle coroutine
        if(idleCoroutineWaiting)
        {
            StopCoroutine(Idle());
            idleCoroutineWaiting = false;
        }
            

        //manually change target and speed
        setter.target = GameObject.Find("Player").transform;
        aiPath.maxSpeed = 3;
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
        detectRange = 2;
        aiPath.maxSpeed = 2;
        rend.sprite = sprites[1];
        chaseTimer = Random.Range(15f, 20f);
    }


    public IEnumerator Idle()
    {
        //worm no longer angry
        rend.sprite = sprites[0];
        setter.target = null;

        //worm randomly chooses spots to idle around, moves slower
        idleTimer = Random.Range(7, 13);
        aiPath.maxSpeed = 2f;



        //Only lets the worm use the "Snake Graph" for node searching (doesnt run into walls)
		GraphMask mask1 = GraphMask.FromGraphName("Snake Graph");

        //Nearest node constraint constructor
		NNConstraint nn = NNConstraint.Default;

        //set the constraint to the Snake Graph
		nn.graphMask = mask1;

        
        
        

        //get a random vector 2 in float range
        //'this' keywords probably moot
        Vector2 spot = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * range;

        //Gets nearest walkable node to the vector3 given
        GraphNode node = AstarPath.active.GetNearest(spot, nn).node;
        
        
        //manually set destination without use of dest setter or seeker
        aiPath.destination = (Vector3)node.position;


        yield return new WaitUntil(() => aiPath.reachedEndOfPath || idleTimer <= 0);

        //Make the worm range larger here again, kind of simulating it having rested
        detectRange = 3;
        idleCoroutineWaiting = false;
    }
}
