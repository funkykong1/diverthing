using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{


    public int currentHealth, maxHealth;

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
        UIManager.UpdateLives(currentHealth);
        
        if(currentHealth > 0)
        {
            onHitWithReference?.Invoke(sender);
        }   
        else
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.GameOver();
            onDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject); 
        }

    }

}
