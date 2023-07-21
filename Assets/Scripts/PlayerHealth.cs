using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int currentHealth = 3;
    private int maxhealth = 3;
    private Rigidbody2D rb;
    public LayerMask[] damageSources;

    public float knockbackForce, knockbackUp;
    private bool hitCooldown;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        
    }
}
