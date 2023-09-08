using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public bool grounded;
    private Gun gun;
    private Rigidbody2D rb;
    public float timer;

    private CircleCollider2D circle;
    private CapsuleCollider2D capsule;



    void Awake()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        circle = GetComponent<CircleCollider2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        circle.enabled = true;
        capsule.enabled = false;

        this.transform.rotation = gun.transform.rotation;

    }

    void Start()
    {   
        //timer and add force to the thing
        timer = 0.5f;
        rb.AddForce(gun.transform.right * gun.force, ForceMode2D.Impulse);
        Physics2D.IgnoreLayerCollision(3,3, true);
    }

    void Update()
    {
        //if timer above 0 tick it down
        if(timer > 0 && !grounded)
            timer -= Time.deltaTime;

        if(timer <= 0 && !grounded)
        {
            DisableHarpoon();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground") && timer >= 0)
        {
            //if recently launched and hits ground, latch on
            grounded = true;
            circle.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            timer = 0;

        }
    }

    public void DisableHarpoon()
    {
        this.tag = default;
            //disable embedding and activate a physical capsule thing
            rb.bodyType = RigidbodyType2D.Dynamic;
            grounded = false;
            capsule.enabled = true;
            circle.enabled = false;
            //give harpoon a bouncy material along with less friction 
            rb.sharedMaterial = new PhysicsMaterial2D("HARPOON MATERIAL");
            rb.sharedMaterial.bounciness = 10;
            rb.sharedMaterial.friction = -1;

            //give harpoon drag to further simulate water and less velocity and force
            rb.drag = 2;
            rb.angularDrag = 1;
    }
}
