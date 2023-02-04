using System;
using UnityEngine;


public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2d;



    private SpriteRenderer sprite;

    [SerializeField]
    private float movingSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private int initHealth;

    private bool isJumping;

    private float moveHorizontal;

    private float currentXvelocity;

    private bool allowMovement;


    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
        allowMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMovement)
        {
            // Debug.Log("HERLP");
            moveHorizontal = Input.GetAxisRaw("Horizontal");  // to move the player left and right with A and D keys
                                             
            if (Input.GetAxisRaw("Jump") > 0 && !isJumping) // to move the player up and with W key
            {
                Debug.Log("Jumping");                    
                rb2d.velocity = new Vector2(currentXvelocity, jumpForce);
            }
        }
   
    }

    private void FixedUpdate()
    {
        //  Debug.Log(rb2d.velocity.x);

        if (rb2d.velocity.sqrMagnitude <= maxSpeed)
        {
            rb2d.AddForce(new Vector2(moveHorizontal, 0f) * movingSpeed);
            
        }
        currentXvelocity = rb2d.velocity.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
      
    }
  
}
