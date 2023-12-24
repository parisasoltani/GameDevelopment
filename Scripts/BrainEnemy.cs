using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainEnemy : Enemy {

    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    private float originalScaleX;
    private Animator animator;

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
                // animator.SetBool("isRunning", true);
            }
            else
            {
                // Stop moving and attack if it's time
                // animator.SetBool("isRunning", false);
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + timeBetweenAttacks;
                    // animator.SetTrigger("isAttacking"); // Trigger the attack animation
                    StartCoroutine(Attack());
                }
            }
        }
    }


    IEnumerator Attack() {

        player.GetComponent<Player>().TakeDamage(damage);

        // Attack animation is already triggered, so we'll just wait for a moment before concluding the attack
        yield return new WaitForSeconds(timeBetweenAttacks);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while(percent <= 1) {
            percent += Time.deltaTime * attackSpeed;
            // float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;

        }

    }


}
