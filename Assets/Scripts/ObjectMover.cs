using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    //floats to determine how far and for how long the object will move
    [SerializeField] private float duration;
    [SerializeField] private float moveUnits;

    //bools for determining the movement of the gameObject
    [SerializeField] private bool moveSideWays;
    [SerializeField] private bool playerOn;
    [SerializeField] private bool drop;
    [SerializeField] private bool notMove;

    //Rigidbody of the gameobject
    private Rigidbody2D rb;

    //orignal positions of the gameobject 
    private Vector3 targetPos;
    private Vector3 startPosition;

    //universal time variable to prevent jerky movement with MovePlatform()
    private float time;

    public void Start()
    {
        //gets the rigid body of the gameobject if it has any
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        //if and else statement to get gameobject's original position according to in which axis is supposed to move
        if (!moveSideWays || drop)
        {
            float y = transform.position.y + moveUnits;
            targetPos = new Vector3(transform.position.x, y, transform.position.z);
            startPosition = transform.position;
        }
        else
        {
            float x = transform.position.x + moveUnits;
            targetPos = new Vector3(x, transform.position.y, transform.position.z);
            startPosition = transform.position;
        }

        //Do you want the player to stand on the platform before moving or do you want the platform to move?
        //if not, then platform will move from the beginning  
        if (!playerOn && !notMove)
            PlatformTrigger();
    }
    //Method for detecting if player has entered the trigger collider of the gameobject and act accordingly 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!drop && playerOn)
            {
                PlatformTrigger();
                playerOn = false;
            }
            else if (drop)
            {
                DropObject();
            }
        }
    }
    //Once it collides with the ground the gameobject will move back to its original spot 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Surface")
        {
            rb.isKinematic = true;
            StartCoroutine(MoveBack());
        }
        else if ((drop && collision.gameObject.tag == "Player"))
        {
            /*Player.player.InstaLifeKill();*/
            rb.isKinematic = true;
            StartCoroutine(MoveBack());
        }
        else if (!drop && playerOn && collision.gameObject.tag == "Player")
        {
            PlatformTrigger();
            playerOn = false;
        }
    }
    //Method call to drop the gameobject by changing its kinematic value
    private void DropObject()
    {
        StopAllCoroutines(); //stops the Coroutine MoveBack to not get Jerky movement
        rb.isKinematic = false;
    }

    //this method is called when the platform comes in contact with the player
    private void PlatformTrigger()
    {
        StartCoroutine(MovePlatform());
    }

    //This method moves the Platform back and forth based on the target and start position of the object 
    private IEnumerator MovePlatform()
    {
        time = 0;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        targetPos = startPosition;
        startPosition = transform.position;
        StartCoroutine(MovePlatform());
    }

    //This method moves the object back to its original spot (only meant for y axis movement) 
    private IEnumerator MoveBack()
    {
        time = 0;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}