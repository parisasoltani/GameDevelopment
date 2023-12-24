using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    [HideInInspector]
    public Transform player;

    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    public int healthPickupChance;
    public GameObject healthPickup;
    
    



 

    // public int healthPickupChance;
    // public GameObject healthPickup;

    public GameObject deathEffect;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    public virtual void TakeDamage(int amount) {
        
        // health
        health -= amount;
        if (health <= 0)
        {


            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            
            Destroy(this.gameObject);
            ScoreManager.instance.AddPoint();


        }
    }
	
}