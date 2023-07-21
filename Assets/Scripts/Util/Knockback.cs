using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float strength, forceUp, delay;

    public bool isHit;

    public UnityEvent onBegin, onDone;

    public void PlayFeedback(GameObject sender)
    {
        if(sender.tag != "Enemy")
            return;

        
        StopAllCoroutines();
        onBegin?.Invoke();
        
        isHit = true;
        Vector2 direction = new Vector2(transform.position.x - sender.transform.position.x, 0);
        rb.velocity += new Vector2(direction.x, forceUp) * strength;

        StartCoroutine(Reset());


    }   

    public IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        //rb.velocity = Vector2.zero;
        isHit = false;
        onDone?.Invoke();
    }

}
