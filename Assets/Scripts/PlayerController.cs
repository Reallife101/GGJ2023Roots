using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputAsset input;
    public InputAction playerMove { get; private set; }
    public InputAction playerJump { get; private set; }

    private Vector3 StartVelocity = Vector3.zero;
    private Vector2 movementVector;
    [Header("Movement Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private float jumpPower;
    //Movement bools
    private bool isGrounded;

    //Grounded things
    [Header("Grounded Check Items")]
    [SerializeField] private Transform groundedCheckObjectLeft;
    [SerializeField] private Transform groundedCheckObjectRight;
    [SerializeField] private LayerMask groundLayer;
    private RaycastHit leftHit;
    private RaycastHit RightHit;

    //Components
    private Rigidbody2D myRB;

    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        input = new PlayerInputAsset();
        playerMove = input.Player.Move;
        playerJump = input.Player.Jump;

        playerJump.started += jumpBehavior =>
        {
            //If grounded, jump normally
            if (isGrounded)
            {
                myRB.AddForce(new Vector2(myRB.velocity.y, jumpPower * 25));
            }
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D resultL = Physics2D.Linecast(transform.position, groundedCheckObjectLeft.position, groundLayer);
        RaycastHit2D resultR = Physics2D.Linecast(transform.position, groundedCheckObjectRight.position, groundLayer);
        isGrounded = resultL || resultR;

        if (isGrounded)
        {
            if (transform.parent != resultL.transform && transform.parent != resultL.transform)
            {
                transform.parent = null;
                transform.localScale = new Vector3(1, 1, 1);
            }
            // only set player's parents to objects if they are on the Platform layer
            if (resultL.transform.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                transform.SetParent(resultL.transform, true);
            }
            else if (resultR.transform.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                transform.SetParent(resultL.transform, true);
            }
        }
        

        Movement();
    }

    private void Movement()
    {
        //Grounded movement
        movementVector = playerMove.ReadValue<Vector2>();
        if (movementVector.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movementVector.x), transform.localScale.y, transform.localScale.z);
        }
        Vector3 VelocityChange = new Vector2(Time.fixedDeltaTime * moveSpeed * 10 * movementVector.x, myRB.velocity.y);
        myRB.velocity = Vector3.SmoothDamp(myRB.velocity, VelocityChange, ref StartVelocity, movementSmoothing);

    }

    private void OnEnable()
    {
        playerMove.Enable();
        playerJump.Enable();
    }

    private void OnDisable()
    {
        playerMove.Disable();
        playerJump.Disable();
    }
}
