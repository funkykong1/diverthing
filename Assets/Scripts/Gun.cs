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

    public float maxDistance = 10f;
    public float force, currDistance, speed, grapplingCooldown;

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
        //POINT AT MOUSE
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartGrapple();
        }
            

        //tick grapple gun timer down
        if(grapplingCooldown >= 0)
            grapplingCooldown -= Time.deltaTime;

        
        if(harpoon != null)
        {
            //enable line renderer and apply to harpoon
            lr.enabled = true;
            lr.SetPosition(1, harpoon.transform.position);

            //get distance to harpoon
            currDistance = Vector2.Distance(this.transform.position, harpoon.transform.position);
        }
        else
        {
            lr.enabled = false;
        }

        
        if(Input.GetKey(KeyCode.Mouse1) && harpoon != null)
        {
            //direction to accelerate when rewinding harpoon cord
            var dir2 = harpoon.transform.position - transform.position;

            //while harpooning, isnt affected by gravity, no need to adjust mass
            rb.gravityScale = 0.001f;

            //linear drag prevents obscene speeds from far away tethers
            rb.drag = currDistance / 1f;

            //apply multipliers for smoother use
            var dir3 = new Vector2(dir2.x*1.5f, dir2.y/1.5f);

            //add force towards harpoon
            rb.AddForce((Vector2)dir3 * speed, ForceMode2D.Force);
        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            //restore ordinary values when not reeling
            rb.drag = 1;
            rb.gravityScale = 0.5f;
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
        rend.sprite = sprites[1];
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

    }

    
    private void ReelIn()
    {
        if(harpoon == null)
            return;
        // Rigidbody2D rb = harpoon.GetComponent<Rigidbody2D>();
        // rb.bodyType = RigidbodyType2D.Dynamic;

    }
}

