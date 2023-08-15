using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // if your character is rotating but off a fixed angle, adjust this value, likely will be 90 or -90 (degrees)
    [SerializeField] int angleOffset;

    public Transform barrel;
    public LayerMask ground;
    private float maxDistance = 10f;
    private Vector2 grapplePoint;

    public float grapplingCooldown;
    private bool grappling;

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
            grapplingCooldown -= 1 * Time.deltaTime;
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
            Debug.Log("Fuckj");
        }

        
    }
}

