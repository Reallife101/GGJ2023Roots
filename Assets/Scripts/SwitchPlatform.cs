using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlatform : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] waypoints;
    private int platformIndex;
    public bool moveInProgress { get; private set;}
    private Coroutine currentCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePlatform()
    {
            //If we have reached the final waypoint, loop back
            if (platformIndex == waypoints.Length - 1)
            {
                platformIndex = 0;
            }
            //Otherwise, proceed to next waypoint
            else
            {
                platformIndex += 1;
            }

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            currentCoroutine = StartCoroutine(Move(waypoints[platformIndex]));
    }

    private IEnumerator Move(Transform destination)
    {
        moveInProgress = true;
        //Move platform over time
        while (this.transform.position != destination.transform.position)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        moveInProgress = false;
    }
}
