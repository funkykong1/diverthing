using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> onHitWithReference, onDeathWithReference;

    [SerializeField]
    private bool isDead = false;

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if(isDead)
            return;
        if(sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;
        
        if(currentHealth > 0)
        {
            onHitWithReference?.Invoke(sender);
        }   
        else
        {
            onDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject); 
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
