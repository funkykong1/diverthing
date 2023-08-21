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

    
    public GameObject harpoonPrefab;
    public Sprite[] sprites;

    private bool grappling;
    private LineRenderer lr;
    private SpriteRenderer rend;
    private GameObject harpoon;
    public float force, currDistance;

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
        {
            
            StartGrapple();
        }
            

        //tick grapple gun timer down
        if(grapplingCooldown >= 0)
            grapplingCooldown -= Time.deltaTime;


        if(harpoon != null)
        {
            lr.enabled = true;
            lr.SetPosition(1, harpoon.transform.position);
            currDistance = Vector2.Distance(this.transform.position, harpoon.transform.position);
        }
        else
        {
            lr.enabled = false;
        }

        if(currDistance <= 0.3)
            StopGrapple();

        
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

        Invoke(nameof(ExecuteGrapple), grappleDelayTime);
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

    }
}

