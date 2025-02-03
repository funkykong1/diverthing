using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Unity;

public class Sparkle : MonoBehaviour
{
    Animator anim;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator>(); 
      StartCoroutine(Spark()); 
    }

    IEnumerator Spark()
    {
        while(true)
        {
            anim.SetTrigger("Sparkle");
            timer = Random.Range(1,3)+ Random.value;
            yield return new WaitForSeconds(timer);
        }
    }
}
