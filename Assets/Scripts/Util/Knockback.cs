using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float strength = 16, delay = 0.15f;

    public UnityEvent onBegin, onDone;

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        onBegin?.Invoke();
        Vector2 direction = (transform.position-sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());


    }   

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        onDone?.Invoke();
    }

}
