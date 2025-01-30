using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement controller;

    public float runSpeed;

    float horizontalMove = 0f;
    bool jump = false;
    
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(horizontalMove != 0)
            anim.speed = 1;
        else
            anim.speed = 0;


        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        //move the guy
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false; 
    }
}
