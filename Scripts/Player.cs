using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveAmount;

    public int health;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator hurtAnim;

    private SceneTransition sceneTransitions;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        sceneTransitions = FindObjectOfType<SceneTransition>();
    }

    // Update is called once per frame
    private void Update()
    {


        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        
        if (moveInput != Vector2.zero)
        {
            

        
            
            //Rotate the player to face left or right depending on the horizontal movement
            if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Face left
            }
            else if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Face right
            }
            anim.SetBool("isRunning", true);
        }
        else {
            anim.SetBool("isRunning", false);
        }
        // Debug.Log("Update - Scale: " + transform.localScale);
    }

    // Anything related to physics should be here
    private void FixedUpdate()
    {
        // Debug.Log("FixedUpdate - Rigidbody2D position: " + rb.position);
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int amount) {
            health -= amount;
            UpdateHealthUI(health);
            hurtAnim.SetTrigger("hurt");
            if (health <= 0){
                Destroy(this.gameObject);
                sceneTransitions.LoadScene("Lose");
            }
        }


    void UpdateHealthUI(int currentHealth) {

            for (int i = 0; i < hearts.Length; i++)
            {

                if (i < currentHealth)
                {
                    hearts[i].GetComponent<Image>().sprite = fullHeart;
                } else {
                    hearts[i].GetComponent<Image>().sprite = emptyHeart;
                }

            }

        }

    public void Heal(int healAmount) {
        if (health + healAmount > 5)
        {
            health = 5;
        } else {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }

	
}

