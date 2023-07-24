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
        if(sender.tag != "Enemy")
            return;

        //prevents plr from getting juggled by 2 separate bits
        Knockback kb = GetComponent<Knockback>();
        if(kb.isHit)
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

}
