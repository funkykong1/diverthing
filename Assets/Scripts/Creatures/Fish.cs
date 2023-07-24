using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class Fish : MonoBehaviour
{
    private Tilemap tilemap;

    private AIPath aiPath;
    private AIDestinationSetter setter;
    private CircleCollider2D circle;
    private CapsuleCollider2D hitBox;
    private SpriteRenderer rend;
    public Sprite[] sprites;

    [SerializeField]
    private float range, idleTimer;
    private bool idling = false;

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

        if(chaseTimer > 0)
            Chase();
        else if (!idling)
        {
            StartCoroutine(Idle());
            rend.sprite = sprites[0];
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
        if(idling)
            StopCoroutine(Idle());

        //manually change target and speed
        setter.target = GameObject.Find("Player").transform;
        aiPath.maxSpeed = 3;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
        //if player re-enters the field of vision, resets timer
        rend.sprite = sprites[1];
        chaseTimer = Random.Range(10f, 18f);
    }
    public IEnumerator Idle()
    {
        //makes a temporary gameobject and goes to it. Idling bool prevents repeats
        idleTimer = Random.Range(5f, 25f);
        aiPath.maxSpeed = 1.5f;
        idling = true;
        Vector2 spot = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * range;
        GameObject point = new GameObject("IDLE POINT");
        point.transform.position = spot;
        setter.target = point.transform;

        yield return new WaitForSeconds(idleTimer);
        idling = false;
        Destroy(point);
    }
}
