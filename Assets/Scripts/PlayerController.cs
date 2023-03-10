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

    //Components
    private Rigidbody2D myRB;

    //Toogling Bool
    private bool isDisabled;

    [SerializeField]
    List<AudioClip> footsteps;

    private int footstepCounter;
    private AudioSource audioPlayer;
    private float footstepTime;

    private Animator ai;
    private SpriteRenderer sr;

    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        input = new PlayerInputAsset();
        playerMove = input.Player.Move;
        playerJump = input.Player.Jump;

        ai = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        footstepCounter = 0;
        footstepTime = 3f;
        audioPlayer = GetComponent<AudioSource>();

        playerJump.started += jumpBehavior =>
        {
            if (isDisabled)
            {
                return;
            }

            //If grounded, jump normally
            if (isGrounded)
            {
                myRB.AddForce(new Vector2(myRB.velocity.y, jumpPower * 25));
            }
        };

        isDisabled = false;
    }

    void playFootstep()
    {
        if (footstepTime > (footsteps[footstepCounter].length+0.15f))
        {
            audioPlayer.PlayOneShot(footsteps[footstepCounter], 1f);
            footstepTime = 0;
            footstepCounter += 1;
            if (footstepCounter == footsteps.Count)
            {
                footstepCounter = 0;
            }
        }
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

        footstepTime = footstepTime + Time.deltaTime;

        if (isGrounded)
        {
            
            if (transform.parent != resultL.transform && transform.parent != resultR.transform)
            {
                transform.parent = null;
                transform.localScale = new Vector3(1, 1, 1);
            }
            // only set player's parents to objects if they are on the Platform layer
            if (resultL.transform != null && resultL.transform.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                transform.SetParent(resultL.transform, true);
            }
            else if (resultR.transform != null && resultR.transform.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                transform.SetParent(resultL.transform, true);
            }
        }
        

        Movement();
    }

    private void Movement()
    {
        if (isDisabled)
        {
            return;
        }

        //Grounded movement
        movementVector = playerMove.ReadValue<Vector2>();
        ai.SetFloat("xVelocity", Mathf.Abs(movementVector.x));
        if (movementVector.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movementVector.x), transform.localScale.y, transform.localScale.z);
            playFootstep();
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


    public void ToggleInput() 
    {
        isDisabled = !isDisabled; 
    }

    public void Disable()
    {
        isDisabled = true;
    }

    public void Enable()
    {
        isDisabled = false;
    }

    public bool getIsDisabled()
    {
        return isDisabled;
    }
}
