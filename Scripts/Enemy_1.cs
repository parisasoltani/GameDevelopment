using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10.0f;
    public float attackRange = 2.0f;
    private Animator animator;
    private float originalScaleX;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Store the original scale
        originalScaleX = transform.localScale.x;
    }

    private void Update()
    {
        // Determine the direction of the player relative to the enemy
        Vector3 direction =  transform.position - player.position;

        // Check the distance to the player and update the animator parameters
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
            
            // Adjust the scaling to face the player
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * originalScaleX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Within attack range, attack the player
            animator.SetBool("isRunning", false);
            animator.SetTrigger("isAttacking"); // Ensure this trigger name matches your Animator
        }
    }
}
