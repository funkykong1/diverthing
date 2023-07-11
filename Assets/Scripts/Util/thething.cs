using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class thething : VersionedMonoBehaviour
{

    public Transform target;
    public float nextWaypointDistance = 3f;
    public float speed = 200f;

    Path path;
    public AstarPath map;


    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    




    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void RunAway()
    {
        // Find the closest node to this GameObject's position
        GraphNode node = AstarPath.active.GetNearest(transform.position).node;

        if (node.Walkable) {
            // Yay, the node is walkable, we can place a tower here or something
        }
    }

    void UpdatePath()
    {
        var nnConstraint = NNConstraint.None;

        this.map.maxNearestNodeDistance = 40;

        nnConstraint.constrainDistance = true;

        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            return;
        }
        else
        {
            //reachedEndOfPath = false;
        }

        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position,path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        //flip the sprite
        if(force.x >= 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if(force.x <= -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
