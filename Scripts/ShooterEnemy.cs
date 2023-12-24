using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy {

    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    private float originalScaleX;
    private Animator animator;

    public Transform shotPoint;
    public GameObject enemyBullet;

    

    public override void Start()
    {
        base.Start(); // Call the base class Start() method
        originalScaleX = transform.localScale.x;

        animator = GetComponent<Animator>();
    }




    private void Update()
    {
        if(player != null)
        {
            // Determine the direction of the player relative to the enemy
            Vector3 direction = player.position - transform.position;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Adjust the scaling to face the player
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * originalScaleX, transform.localScale.y, transform.localScale.z);

            if (distanceToPlayer > stopDistance)
            {
                // Enemy should move
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                animator.SetBool("isRunning", true);
            }
            else
            {
                // Stop moving and attack if it's time
                animator.SetBool("isRunning", false);
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + timeBetweenAttacks;
                    animator.SetTrigger("isAttacking"); // Trigger the attack animation
                    StartCoroutine(ShooterAttack());
                }
            }
        }
    }


    IEnumerator ShooterAttack() {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);

        attackTime = Time.time + timeBetweenAttacks; 
    
        yield return new WaitForSeconds(timeBetweenAttacks); // Add a delay here for time between shots, if needed
}


        }

    



