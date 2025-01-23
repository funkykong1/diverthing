using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // if your character is rotating but off a fixed angle, adjust this value, likely will be 90 or -90 (degrees)
    [SerializeField] int angleOffset;

    public Transform barrel;
    public LayerMask ground;
    private Vector2 grapplePoint;

    private GameObject harpoon, player;
    public GameObject harpoonPrefab;

    public Sprite[] sprites;

    private bool grappling;

    private LineRenderer lr;
    private SpriteRenderer rend;

    //Force is gun strength, speed is how fast player moves via grapple
    //reel speed is how fast harpoon gets reeled in
    public float force, currDistance, speed ,reelSpeed;
    private bool forceReturn;
    private Rigidbody2D rb;


    void Start()
    {
        //init
        rend = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(harpoon == null)
                StartGrapple();
            else
                Release();


        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            //restore ordinary values when not Releaseg
            rb.drag = 1;
            rb.gravityScale = 0.5f;
            //RESTORRE HARPOON VALUE TOO
            if (harpoon != null)
            {
                harpoon.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            }
        }

        

        
    }


    //fixedupdate ignores fps drops
    void FixedUpdate()
    {

        //POINT AT MOUSE
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //IF HARPOON EXISTS, DO THESE
        if(harpoon != null)
        {
            //enable line renderer and apply to harpoon
            lr.enabled = true;
            lr.SetPosition(1, harpoon.transform.position);

            //get distance to harpoon
            currDistance = Vector2.Distance(this.transform.position, harpoon.transform.position);

            if(harpoon.GetComponent<Harpoon>().grounded)
                rend.sprite = sprites[1];
            else
                rend.sprite = sprites[2];
                

            if(currDistance > 15)
                forceReturn = true;

            if(forceReturn)
            {
                ForceReturn();
            }
            
        }

        else
        {
            lr.enabled = false;
        }

        //handles how player interacts with the harpoon
        if(Input.GetKey(KeyCode.Mouse1) && harpoon != null)
        {
            if(harpoon.GetComponent<Harpoon>().grounded)
                ReelIn();
            else
                Retrieve();
        }
    }

    void LateUpdate()
    {
        if(grappling)
            lr.SetPosition(0, barrel.position);
    }

    void StartGrapple()
    {
        if(grappling)
            return;

        Invoke(nameof(ExecuteGrapple), 0);
    }

    private void ExecuteGrapple()
    {
        if(grappling)
            return;
        grappling = true;
        harpoon = Instantiate(harpoonPrefab, barrel.position, Quaternion.identity);

        lr.enabled = true;
        lr.SetPosition(1, harpoon.transform.position);
    }


    //Grappling hook gun reload here
    private void StopGrapple()
    {
        rend.sprite = sprites[0];
        Destroy(harpoon);
        grappling = false;
        lr.enabled = false;
        forceReturn = false;
    }

    //release harpoon from the ground
    private void Release()
    {
        harpoon.GetComponent<Harpoon>().DisableHarpoon();
    }


    //reel towards harpoon
    private void ReelIn()
    {
        //direction to accelerate when rewinding harpoon cord
        var dir2 = harpoon.transform.position - transform.position;

        //while harpooning, isnt affected by gravity, no need to adjust mass
        rb.gravityScale = 0.001f;

        //linear drag prevents obscene speeds from far away tethers
        rb.drag = currDistance * 1.2f;

        //apply multipliers for smoother use
        var dir3 = new Vector2(dir2.x*1.5f, dir2.y/1.5f);

        //add force towards harpoon
        rb.AddForce(dir3 * speed, ForceMode2D.Force);
    }


    //reel harpoon towards player
    private void Retrieve()
    {
        Rigidbody2D rbh = harpoon.GetComponent<Rigidbody2D>();
        //direction to accelerate when rewinding harpoon cord
        var dir2 = transform.position - harpoon.transform.position;

        rbh.gravityScale = 0.0001f;


        //incrementally add or remove harpoon speed
        //harpoon is extremely close, apply more speed
        if(currDistance <= 2)
            reelSpeed = 10;

        else if(currDistance <= 4)
            //harpoon is semi close, lessen speed
            reelSpeed = 6;
        else
            //harpoon is far away, reduce speed, dir2 will be huge at this point anyway
            reelSpeed = 5;
            
        //add force into harpoon 
        rbh.AddForce((Vector2)dir2*reelSpeed, ForceMode2D.Force);

        //if harpoon close enough, reload 
        if(currDistance <= 0.5f)
            StopGrapple();

    }   

    //if distance grows too large, force harpoon to return to prevent softlocking
    private void ForceReturn()
    {
        if(!forceReturn)
            return;
        else
        {
            Retrieve();
            Release();
        }
            

        harpoon.GetComponent<CapsuleCollider2D>().enabled = false;
        
    }
}

