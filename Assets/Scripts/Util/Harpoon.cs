using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public bool isConnected;
    private Gun gun;
    private Rigidbody2D rb;
    private float timer;

    private BoxCollider2D box;
    private CapsuleCollider2D capsule;

    void Awake()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        box = GetComponent<BoxCollider2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        box.enabled = false;
        capsule.enabled = true;

        this.transform.rotation = gun.transform.rotation;

    }

    void Start()
    {   
        timer = 2.5f;
        rb.AddForce(gun.transform.right * gun.force, ForceMode2D.Impulse);

        Destroy(gameObject, 5);
    }

    void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;

        if(timer <= 0)
        {
            //disable embedding and activate a physical box thing
            capsule.enabled = false;
            box.enabled = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground") && timer >= 0)
        {
            //if recently launched and hits ground, latch on
            isConnected = true;
        }
    }
}