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

    public UnityEvent onBegin, onDone;

    public void PlayFeedback(GameObject sender)
    {
        if(sender.tag != "Enemy")
            return;

        StopAllCoroutines();
        onBegin?.Invoke();
        
        Vector2 direction = new Vector2(transform.position.x - sender.transform.position.x, 0);
        rb.velocity += new Vector2(direction.x, forceUp) * strength;

        StartCoroutine(Reset());


    }   

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        onDone?.Invoke();
    }

}
