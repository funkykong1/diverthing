using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{


    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float strength, forceUp, iFrameDuration, flashDelay;

    public bool isHit;

    public UnityEvent onBegin, onDone;
    private SpriteRenderer rend,gun;

    //transparency for milder iframe effect
    private Color opaque = new Color(255, 255, 255, 1);
    private Color transparent = new Color(255, 255, 255, 0.5f);


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        gun = GameObject.Find("Gun").GetComponent<SpriteRenderer>();
    }

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        onBegin?.Invoke();
        
        isHit = true;
        Vector2 direction = new Vector2(transform.position.x - sender.transform.position.x, 0);
        rb.velocity += new Vector2(direction.x, forceUp) * strength;

        StartCoroutine(Reset());


    }   

    public IEnumerator Reset()
    {
        //iframe flashing
    for (float i = 0; i < iFrameDuration; i += flashDelay)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (rend.color == opaque)
            {
                
                gun.color = transparent;
                rend.color = transparent;
            }
            else
            {
                gun.color = opaque;
                rend.color = opaque;
            }
            yield return new WaitForSeconds(flashDelay);
        }
        //i frames end here
        rend.color = opaque;
        gun.color = opaque;
        isHit = false;
        onDone?.Invoke();
    }

}
