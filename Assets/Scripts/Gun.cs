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


    // Update is called once per frame
    void Update()
    {
        //POINT AT MOUSE
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //


    }

    void StartGrapple()
    {
        RaycastHit2D hit =  Physics2D.Raycast(barrel.position, Vector3.forward, maxDistance, 6);
        if(hit)
        {
            grapplePoint = hit.point;
            
        }
    }
}

