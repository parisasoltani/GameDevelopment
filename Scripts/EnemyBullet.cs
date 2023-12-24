using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    Player playerScript;
    Vector2 targetPosition;

    public float speed;
    public int damage;

    public GameObject effect;
    public GameObject deathEffect;
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        // Check if the bullet is close enough to the target position to consider it "arrived".
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) // Use a small threshold instead of exact equality
        {
            if (effect != null)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
