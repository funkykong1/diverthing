using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // if your character is rotating but off a fixed angle, adjust this value, likely will be 90 or -90 (degrees)
    [SerializeField] int angleOffset;

    public Transform barrel;
    public LayerMask ground;
    public float maxDistance = 10f;
    private Vector2 grapplePoint;

    public float grapplingCooldown, grappleDelayTime;

    
    public GameObject harpoon;
    public Sprite[] sprites;

    private bool grappling;
    private LineRenderer lr;
    private SpriteRenderer rend;
    private GameObject activeHarpoon;
    public float force;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
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
            StartGrapple();

        //tick grapple gun timer down
        if(grapplingCooldown >= 0)
            grapplingCooldown -= Time.deltaTime;


        if(activeHarpoon != null)
        {
            lr.enabled = true;
            lr.SetPosition(1, activeHarpoon.transform.position);
        }
        else
        {
            lr.enabled = false;
        }


    }

    void LateUpdate()
    {
        if(grappling)
            lr.SetPosition(0, barrel.position);
    }

    void StartGrapple()
    {
        if(grapplingCooldown > 0)
            return;
        
        grappling = true;

        RaycastHit2D hit;
        hit = Physics2D.Raycast((Vector2)barrel.position, (Vector2)barrel.transform.right, maxDistance, LayerMask.GetMask("Ground"));
        
        if(hit)
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = barrel.position + barrel.right * maxDistance;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        
    }

    private void ExecuteGrapple()
    {
        // if(grappling)
        //     return;
        rend.sprite = sprites[1];
        grappling = true;
        activeHarpoon = Instantiate(harpoon, barrel.position, Quaternion.identity);

        lr.enabled = true;
        lr.SetPosition(1, activeHarpoon.transform.position);
    }

    private void StopGrapple()
    {
        if(!grappling)
            return;

        //WHEN THING REELED IN
        rend.sprite = sprites[0];
        grappling = false;

        grapplingCooldown = 2;

        lr.enabled = false;

    }
}

